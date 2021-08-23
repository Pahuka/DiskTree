using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiskTree
{
    class Directory
    {
        private string dir;
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

            if (input.Count > 1)
               SubDir.Add(new Directory(CalculateDirs(input, 1).Skip(1).ToList()));
        }

        public void SortDirs(List<Directory> newDir)
        {
            var tempDir = SubDir.Where(x => x.Dir == newDir.FirstOrDefault().Dir).FirstOrDefault();

            if (tempDir != null)
                tempDir.SortDirs(newDir.FirstOrDefault().SubDir);

            else SubDir.Add(newDir.FirstOrDefault());
            SubDir.Sort((a, b) => { return string.Compare(a.Dir, b.Dir, StringComparison.Ordinal); });
        }

        private List<string> CalculateDirs(List<string> input, int index)
        {
            for (int i = index; i < input.Count(); i++)
                input[i] = input[i].Insert(0, " ");
            if (index < input.Count())
                CalculateDirs(input, index + 1);

            return input;
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
                {
                    list[temp.Dir].SortDirs(temp.SubDir);
                }
                else list.Add(temp.Dir, temp);
            }

            return null;
        }
    }
}