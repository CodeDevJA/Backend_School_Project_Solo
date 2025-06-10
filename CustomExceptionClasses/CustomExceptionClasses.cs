// Folder Exceptions:
// - FolderNotFoundException
// - FolderAlreadyExistsException
// - FolderAccessDeniedException

// File Exceptions:
// - FileNotFoundException
// - FileAlreadyExistsException
// - FileAccessDeniedException

// General Exception:
// - OperationFailedException (for unexpected failures)

public class FolderNotFoundException : Exception
{
    public FolderNotFoundException(string message) : base(message) { }
}

public class FolderAlreadyExistsException : Exception 
{ 
    public FolderAlreadyExistsException(string message) : base(message) { } 
}

public class FolderAccessDeniedException : Exception 
{ 
    public FolderAccessDeniedException(string message) : base(message) { } 
}

public class FileNotFoundException : Exception 
{ 
    public FileNotFoundException(string message) : base(message) { } 
}

public class FileAlreadyExistsException : Exception 
{ 
    public FileAlreadyExistsException(string message) : base(message) { } 
}

public class FileAccessDeniedException : Exception 
{ 
    public FileAccessDeniedException(string message) : base(message) { } 
}

public class OperationFailedException : Exception
{
    public OperationFailedException(string message) : base(message) { }
}
