using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandoraBox.Extensions;
using System.Reflection;

namespace PandoraBox.DataBuilds.Formatter
{
    public class ExcelFormatter : BaseFormatter
    {
        private int _curRowNumber;
        private int _curCellNumber;
        private ExcelPackage _container;

        public ExcelFormatter(FormatOption option) : base(option) { }

        protected override void BeginComponent()
        {
            _curRowNumber = _curCellNumber = 1;
            _container = new ExcelPackage(new FileInfo(base.Option.FilePath));
            var addedWs = _container.Workbook.Worksheets[base.Option.SheetName];
            if (_container.Workbook.Worksheets[base.Option.SheetName] == null)
                addedWs = _container.Workbook.Worksheets.Add(base.Option.SheetName);
        }

        protected override void DisposeComponent()
        {
            _container.Dispose();
            _container = null;
        }

        protected override void SaveComponent()
        {
            _container.Save();
        }

        protected override void WriteByComponent(string combinedString)
        {
            var splitedString = combinedString.Split(base.Option.Separator);
            var addedWs = _container.Workbook.Worksheets[base.Option.SheetName];
            foreach (var eachString in splitedString)
            {
                addedWs.SetValue(_curRowNumber, _curCellNumber++, eachString);
            }

            _curRowNumber++;
            _curCellNumber = 1;
        }

        public void WriteExcel(int rowIdx, int colIdx, object value)
        {
            var addedWs = _container.Workbook.Worksheets[base.Option.SheetName];
            addedWs.SetValue(rowIdx, colIdx, value);
        }

        public void WriteExcel(string cellAddress, object value)
        {
            var addedWs = _container.Workbook.Worksheets[base.Option.SheetName];
            addedWs.SetValue(cellAddress, value);
        }

        public object ReadExcel(int rowIdx, int colId, string sheetName = "")
        {
            if (sheetName.IsNullOrEmpty())
            {
                sheetName = base.Option.SheetName;
            }

            var readWs = _container.Workbook.Worksheets[sheetName];
            return readWs.GetValue(rowIdx, colId);
        }

        public T ReadExcel<T>(int rowIdx, int colId, string sheetName = "")
        {
            if (sheetName.IsNullOrEmpty())
            {
                sheetName = base.Option.SheetName;
            }

            var readWs = _container.Workbook.Worksheets[sheetName];
            return readWs.GetValue<T>(rowIdx, colId);
        }

        public IEnumerable<T> ReadAsList<T>(int titleRow = 1, string sheetName = "") where T : new()
        {
            if (sheetName.IsNullOrEmpty())
            {
                sheetName = base.Option.SheetName;
            }

            var tpMapping = GetTitlePropMaps(titleRow, typeof(T));
            var readWs = _container.Workbook.Worksheets[sheetName];
            for (var rowIdx = titleRow + 1; rowIdx <= readWs.Dimension.End.Row; rowIdx++)
            {
                var newRow = new T();
                for (var colIdx = readWs.Dimension.Start.Column; colIdx <= readWs.Dimension.Columns; colIdx++)
                {
                    PropertyInfo mapProp;
                    if (tpMapping.TryGetValue(colIdx, out mapProp))
                    {
                        mapProp.SetValue(newRow, readWs.GetValue<string>(rowIdx, colIdx));
                    }
                }

                yield return newRow;
            }
        }

        private Dictionary<int, PropertyInfo> GetTitlePropMaps(int titleRow, Type mappingObjType)
        {
            var tpMapping = new Dictionary<int, PropertyInfo>();
            var readWs = _container.Workbook.Worksheets[1];
            var props = mappingObjType.GetProperties();
            for (var rowIdx = readWs.Dimension.Start.Column; rowIdx <= readWs.Dimension.Columns; rowIdx++)
            {
                var checkingValue = readWs.GetValue<string>(titleRow, rowIdx);
                if (props.Count(p => p.Name.Equals(checkingValue)) > 0)
                {
                    tpMapping.Add(rowIdx, props.First(p => p.Name.Equals(checkingValue)));
                }
            }

            return tpMapping;
        }
    }
}
