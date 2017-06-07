using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandoraBox.Extensions;

namespace PandoraBox.DataBuilds.Formatter
{
    public class ExcelFormatter : BaseFormatter
    {
        private int _rowNumber;
        private int _cellNumber;
        private ExcelPackage _container;

        public ExcelFormatter(FormatOption option) : base(option) { }

        protected override void BeginComponent()
        {
            _rowNumber = _cellNumber = 1;
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
                addedWs.SetValue(_rowNumber, _cellNumber++, eachString);
            }

            _rowNumber++;
            _cellNumber = 1;
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
    }
}
