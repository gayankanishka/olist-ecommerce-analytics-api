namespace Olist.Ecommerce.Analytics.Infrastructure.Hadoop
{
    /// <summary>
    /// Holds the Hadoop operations constants.
    /// </summary>
    public static class HdfsOperations
    {
        /// <summary>
        /// Opens a file in the Hadoop filesystem and read.
        /// </summary>
        public static string OpenAndRead => "OPEN";
        
        /// <summary>
        /// View the status of the Hadoop filesystem's specified directory.
        /// </summary>
        public static string FileOrDirectoryStatus => "GETFILESTATUS";
        
        /// <summary>
        /// List the content of the Hadoop filesystem's specified directory.
        /// </summary>
        public static string ListDirectory => "LISTSTATUS";
    }
}
