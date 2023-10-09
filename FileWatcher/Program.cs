// Get Current Directory...
var path = Directory.GetCurrentDirectory();
var watcher = new FileSystemWatcher(path, "*.pdf")
{
    NotifyFilter = NotifyFilters.FileName,
    EnableRaisingEvents = true
};

// Subscribe to the events
watcher.Changed += OnChanged;
watcher.Created += OnCreated;
watcher.Deleted += OnDeleted;
watcher.Renamed += OnRenamed;

Console.WriteLine("File System Watcher is running. Press Enter to exit...");
Console.WriteLine($"Watching Directory: {path}");
Console.ReadLine();


static void OnChanged(object sender, FileSystemEventArgs e)
{
    Thread.Sleep(1000);
    long fileSize = GetFileSize(e.FullPath);
    Console.WriteLine($"File changed: {e.FullPath}, Size: {fileSize} bytes");
}

static void OnCreated(object sender, FileSystemEventArgs e)
{
    Thread.Sleep(1000);
    long fileSize = GetFileSize(e.FullPath);
    Console.WriteLine($"File created: {e.FullPath}, Size: {fileSize} bytes");
}

static void OnDeleted(object sender, FileSystemEventArgs e)
{
    Thread.Sleep(1000);
    Console.WriteLine($"File deleted: {e.FullPath}");
}

static void OnRenamed(object sender, RenamedEventArgs e)
{
    Thread.Sleep(1000);
    long fileSize = GetFileSize(e.FullPath);
    Console.WriteLine($"File renamed from {e.OldFullPath} to {e.FullPath}, Size: {fileSize} bytes");
}

static long GetFileSize(string filePath)
{
    try
    {
        FileInfo fileInfo = new FileInfo(filePath);
        if (fileInfo.Exists)
        {
            return fileInfo.Length;
        }
    }
    catch (Exception)
    {
        // Handle exceptions if the file cannot be accessed
    }

    return -1; // Return -1 to indicate file size couldn't be determined
}