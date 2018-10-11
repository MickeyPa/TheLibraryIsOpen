﻿namespace TheLibraryIsOpen.Models.DBModels
{
    public class Music
    {
        public int MusicId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Label { get; set; }
        public string ReleaseDate { get; set; }
        public string Asin { get; set; }

        // Default constructor
        public Music() {}

        /* Here the constructor assign values to attributes besides musicId.
         * The musicId is generated by database, when insert an entry to the "music" table (assumed it's already & primary key autoincrement).
         * The last id just entered from table would be assigned to musicId for the music object. So that to avoid same id appears when server gets restarted.
        */
        public Music(string type, string title, string artist, string label, string releaseDate, string asin)
        {
            Type = type;
            Title = title;
            Artist = artist;
            Label = label;
            ReleaseDate = releaseDate;
            Asin = asin;
        }

        // another construcor who  assigns music id is added as requested.
        public Music(int mId, string type, string title, string artist, string label, string releaseDate, string asin)
        {
            MusicId = mId;
            Type = type;
            Title = title;
            Artist = artist;
            Label = label;
            ReleaseDate = releaseDate;
            Asin = asin;
        }

        // Return information about the music
        public override string ToString()
        {
            return "Music:\nType:" + Type + "\nTitle:" + Title + "\nArtist:" + Artist + "\nLabel: " + Label + 
                "\nRelease Date:" + ReleaseDate + "\nASIN:" + Asin + "\nMusic ID:" + MusicId;
        }
    }
}