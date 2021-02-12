using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace EmbeddingJSComponentsWASM_PSInShared.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new EmbeddingJSComponentsWASM_PSInShared.App(), args);
            host.Run();
        }
    }
}
