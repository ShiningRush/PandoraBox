using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoraBox.DataBuilds.Formatter
{
    public class TxtFormatter : BaseFormatter
    {
        private FileStream _container;

        public TxtFormatter(FormatOption option) : base(option) { }

        protected override void BeginComponent()
        {
            _container = File.Open(base.Option.FilePath, FileMode.OpenOrCreate);
        }

        protected override void DisposeComponent()
        {
            _container.Dispose();
            _container = null;
        }

        protected override void SaveComponent()
        {
            _container.Close();
        }

        protected override void WriteByComponent(string combinedString)
        {
            var writingData = Encoding.UTF8.GetBytes(combinedString + "\r\n");
            _container.WriteAsync(writingData, 0, writingData.Length);
        }

        public void WriteLine(string lineString)
        {
            var writingData = Encoding.UTF8.GetBytes(lineString + "\r\n");
            _container.WriteAsync(writingData, 0, writingData.Length);
        }

        public void WriteText(string text)
        {
            var writingData = Encoding.UTF8.GetBytes(text);
            _container.WriteAsync(writingData, 0, writingData.Length);
        }
    }
}
