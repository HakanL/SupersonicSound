﻿using FMOD;

namespace SupersonicSound.Exceptions
{
    public class FmodFileNotFoundException
        : FmodFileException
    {
        public string FileName { get; private set; }

        public FmodFileNotFoundException(string fileName = null)
            : base(RESULT.ERR_FILE_NOTFOUND)
        {
            FileName = fileName;
        }
    }
}
