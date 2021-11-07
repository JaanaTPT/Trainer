using System.Collections.Generic;
using System.IO;

namespace Trainer.FileAccess
{
    public interface IFileClient
    {
        Stream Get(string container, string fileName);
        void Save(string container, string fileName, Stream inputStream);
        void Delete(string container, string fileName);
        IList<string> List(string container, string prefix);
    }
}
