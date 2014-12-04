using System;

namespace Theatre.Conversion.Utility
{
    internal interface IMediaInfo
    {
        int Open(String FileName);
        int Count_Get(StreamKind StreamKind);
        String Get(StreamKind StreamKind, int StreamNumber, String Parameter);
        void Close();
        String Option(String Option, String Value);
    }
}
