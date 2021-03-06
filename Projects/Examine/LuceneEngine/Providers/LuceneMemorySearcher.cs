using System.Diagnostics;
using System.IO;
using System.Security;
using Lucene.Net.Analysis;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;

namespace Examine.LuceneEngine.Providers
{
    /// <summary>
    /// A Lucene searcher that uses RAMDirectory
    /// </summary>
    public class LuceneMemorySearcher : LuceneSearcher
    {
        private readonly Directory _luceneDirectory;

        /// <summary>
        /// Default constructor
        /// </summary>
        public LuceneMemorySearcher()
        {
        }

        /// <summary>
        /// Constructor to allow for creating an indexer at runtime
        /// </summary>
        /// <param name="luceneDirectory"></param>
        /// <param name="analyzer"></param>
		[SecuritySafeCritical]
        public LuceneMemorySearcher(Directory luceneDirectory, Analyzer analyzer)
        {
            _luceneDirectory = new RAMDirectory(luceneDirectory);;
            IndexingAnalyzer = analyzer;
        }

		[SecuritySafeCritical]
        protected override Directory GetLuceneDirectory()
        {
            return _luceneDirectory;
        }

        private IndexSearcher _searcher;

        /// <summary>
        /// Gets the searcher for this instance
        /// </summary>
        /// <returns></returns>
		[SecuritySafeCritical]
        public override Searcher GetSearcher()
        {
            //Debug.WriteLine("LuceneMemorySearcher.GetSearcher");
            if (_searcher == null)
            {
                 _searcher = new IndexSearcher(GetLuceneDirectory(), true);
            }
            //ensure scoring is turned on for sorting
            _searcher.SetDefaultFieldSortScoring(true, true);
            return _searcher;
        }
    }
}