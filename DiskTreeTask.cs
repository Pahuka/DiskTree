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
        public Directory SubDir;

        public string Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        public Directory() { }

        public Directory(List<string> input)
        {
            if (input.Count > 0)
            {
                dir = input.First();
                SubDir = new Directory(CalculateDirs(input, 1).Skip(1).ToList());
            }
        }

        private List<string> CalculateDirs(List<string> input, int index)
        {
            for (int i = index; i < input.Count(); i++)
                input[i] = input[i].Insert(0, " ");
            if (index < input.Count()) CalculateDirs(input, index + 1);

            return input;
        }
    }

    static class DiskTreeTask
    {
        public static List<string> Solve(List<string> input)
        {
            var list = new List<Directory>();
            var result = new List<string>();

            foreach (var dirs in input)
            {
                list.Add(new Directory(dirs.Split('\\').ToList()));
            }

            list.Sort((a, b) => { return string.Compare(a.Dir, b.Dir, StringComparison.Ordinal); });


            foreach (var dirs in list)
            {
                if (dirs.SubDir.Dir == null) result.Add(dirs.Dir);
                else
                {
                    if (!result.Contains(dirs.Dir)) result.Add(dirs.Dir);
                    result.AddRange(Sort(list.Where(x => x.Dir == dirs.Dir && x.SubDir.Dir != null).ToList()));
                }
            }

            return null;
        }

        public static List<string> Sort(List<Directory> list)
        {
            var result = new List<string>();

            foreach (var item in list.OrderBy(x => x.SubDir.Dir, StringComparer.Ordinal))
            {
                var stepDir = item.SubDir;
                while (stepDir.Dir != null)
                {
                    result.Add(stepDir.Dir);
                    stepDir = stepDir.SubDir;
                }
            }

            return result;
        }
    }
}