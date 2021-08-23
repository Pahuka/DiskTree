using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    class Directory
    {
        private string dir;
        private List<string> print;
        public List<Directory> SubDir;

        public string Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        public Directory() { }

        public Directory(List<string> input)
        {
            SubDir = new List<Directory>();
            dir = input.First();
            input.RemoveAt(0);

            if (input.Count > 0)
                SubDir.Add(new Directory(input.Select(x => " " + x).ToList()));
        }

        public List<string> PrintDirs()
        {
            print = new List<string>();
            print.Add(Dir);

            foreach (var item in this.SubDir)
            {
                print.AddRange(item.PrintDirs());
            }

            return print;
        }

        public void SortDirs(List<Directory> newDir)
        {
            if (newDir.Count == 0) return;

            var tempDir = SubDir.Where(x => x.Dir == newDir.FirstOrDefault().Dir).FirstOrDefault();

            if (tempDir != null)
                tempDir.SortDirs(newDir.FirstOrDefault().SubDir);
            else SubDir.Add(newDir.FirstOrDefault());
            SubDir.Sort((a, b) => { return string.Compare(a.Dir, b.Dir, StringComparison.Ordinal); });
        }
    }

    static class DiskTreeTask
    {
        public static List<string> Solve(List<string> input)
        {
            var list = new SortedDictionary<string, Directory>();
            var result = new List<string>();

            foreach (var dirs in input)
            {
                var temp = new Directory(dirs.Split('\\').ToList());
                if (list.ContainsKey(temp.Dir))
                    list[temp.Dir].SortDirs(temp.SubDir);
                else list.Add(temp.Dir, temp);
            }

            foreach (var item in list.Values)
            {
                result.AddRange(item.PrintDirs());
            }

            return result;
        }
    }
}