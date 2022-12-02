////////////////////////////
// Classes/BloomFilter.cs //
////////////////////////////

// install dependencies
using System.Text;

// suppress warnings
#pragma warning disable 

namespace BloomFilter
{

    class BloomFilter
    {
        protected int[] filter = new int[2682974];

        // FIRST TIME USE
        // 
        // constructor used to reset all indexes in the filter to 0
        public BloomFilter(int num)
        {
            for (int a = 0; a < filter.Length; a++)
            {
                filter[a] = 0;
            }
        }

        // reads the list of words from text file
        // inserts each word into filter
        // write the filter to a text file
        public void SetUp()
        {
            string[] words = File.ReadAllLines("Resources/TrueWords.txt");

            foreach (var a in words)
            {
                Insert(a);
            }

            WriteFilter();
        }

        // write the filter to a text file
        protected void WriteFilter()
        {
            string filename = "Resources/Filter.txt";
            string[] stringArray = filter.Select(x => x.ToString()).ToArray();
            string result = String.Concat(stringArray);

            File.WriteAllText(filename, result);
        }

        // GENERAL USE
        // 
        // read the filter from a text file and store as an int array
        public BloomFilter()
        {
            // change filename to the <filter>.txt 
            string filename = "Resources/Filter.txt";
            string text = File.ReadAllText(filename);

            for (uint a = 0; a < filter.Length; a++)
            {
                filter[a] = text[(int)a] - 48;
            }
        }

        public bool Lookup(string word)
        {
            // there are only two one letter words that are in the set - "a" and "i"
            // however all one letter words return true, so check all one letter words
            if (word.Length == 1)
            {
                return (word == "a" || word == "i");
                return false;
            }
            else
            {
                // if all the indexes are 1, then the item may be in the set
                return (filter[Hash1(word)] == 1 && filter[Hash2(word)] == 1 && filter[Hash3(word)] == 1 && filter[Hash4(word)] == 1 && filter[Hash5(word)] == 1 && filter[Hash6(word)] == 1 && filter[Hash7(word)] == 1 && filter[Hash8(word)] == 1 && filter[Hash9(word)] == 1 && filter[Hash10(word)] == 1);
            }

            // if any of the indexes are 0, then the item is definitely not in the set
            return false;
        }

        public void Insert(string word)
        {
            filter[Hash1(word)] = 1;
            filter[Hash2(word)] = 1;
            filter[Hash3(word)] = 1;
            filter[Hash4(word)] = 1;
            filter[Hash5(word)] = 1;
            filter[Hash6(word)] = 1;
            filter[Hash7(word)] = 1;
            filter[Hash8(word)] = 1;
            filter[Hash9(word)] = 1;
            filter[Hash10(word)] = 1;
        }

        // Pearson Hashing
        protected uint Hash1(string word)
        {
            uint hash = 0;
            uint index;
            byte[] bytes = Encoding.UTF8.GetBytes(word);
            byte[] nums = { 114, 177, 249, 4, 222, 117, 190, 121, 130, 78, 53, 196, 255, 208, 5, 116, 221, 27, 144, 41, 252, 33, 170, 231, 62, 89, 235, 111, 174, 57, 105, 132, 204, 205, 151, 135, 90, 211, 37, 36, 66, 164, 40, 253, 108, 153, 98, 156, 67, 214, 35, 6, 38, 42, 162, 148, 28, 18, 254, 79, 61, 155, 3, 25, 184, 189, 152, 143, 84, 216, 87, 44, 75, 138, 191, 158, 243, 230, 1, 242, 91, 113, 26, 171, 245, 197, 22, 68, 187, 161, 218, 246, 97, 16, 234, 193, 73, 125, 101, 80, 226, 195, 139, 49, 9, 212, 224, 63, 72, 13, 100, 233, 104, 163, 207, 247, 137, 199, 136, 160, 203, 141, 250, 71, 200, 167, 129, 32, 19, 145, 238, 43, 142, 237, 198, 64, 76, 103, 182, 149, 2, 74, 107, 124, 88, 54, 157, 159, 51, 52, 102, 201, 7, 77, 180, 110, 109, 228, 85, 99, 11, 239, 169, 12, 8, 209, 165, 168, 248, 34, 82, 112, 140, 56, 120, 185, 55, 58, 31, 179, 47, 213, 86, 206, 194, 69, 127, 147, 123, 20, 219, 166, 29, 223, 220, 83, 70, 225, 188, 60, 21, 251, 240, 10, 119, 122, 23, 131, 96, 178, 227, 126, 173, 14, 17, 176, 192, 15, 46, 65, 215, 134, 232, 115, 106, 181, 175, 48, 202, 154, 150, 81, 50, 183, 39, 229, 92, 24, 217, 45, 172, 95, 128, 93, 133, 244, 210, 186, 118, 59, 30, 241, 146, 236, 94 };

            foreach (var a in bytes)
            {
                // index = (hash ^ a) % (uint)nums.Length;
                // hash = nums[index];

                hash = nums[((hash ^ a) % (uint)nums.Length)];
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Hashing by cyclic polynomial (Buzhash)
        protected uint Hash2(string word)
        {
            uint hash = 1;

            foreach (char a in word)
            {
                hash = CircularShift(hash) ^ CircularShift((byte)a) ^ CircularShift((uint)(byte)a + 1);
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Fowler-Noll-Vo (FNV-0) hash 
        protected uint Hash3(string word)
        {
            uint hash = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var a in bytes)
            {
                // hash *= 16777619;
                // hash ^= a;

                hash = (hash * 16777619) ^ a;
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // dijb2
        protected uint Hash4(string word)
        {
            // randomly generated number
            uint hash = 5381;
            uint a;
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var b in bytes)
            {
                hash = ((hash << 5) + hash) + 33;
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // sdbm
        protected uint Hash5(string word)
        {
            uint hash = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var a in bytes)
            {
                hash = 65599 + (hash << 6) + (hash << 16) - hash;
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // PJW hash function
        protected uint Hash6(string word)
        {
            uint hash = 0;
            uint bits = (sizeof(uint) * 8);
            uint max = (uint)(0xFFFFFFFF) << (int)(bits - (bits / 8));

            for (int a = 0; a < word.Length; a++)
            {
                hash = hash << (int)(bits / 8) + word[a];

                if (max != 0)
                {
                    hash = hash ^ (max >> (int)bits * 3 / 4) & (~max);
                }
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Fast-Hash
        protected uint Hash7(string word)
        {
            // randomly generated number
            uint a = unchecked((uint)0x880355f21e6d1965);
            uint hash = 144 ^ ((uint)word.Length * a);
            uint b = 0;

            for (uint c = 0; c < word.Length; c++)
            {
                hash = (hash ^ Mix(c)) * a;
            }

            int d = word.Length & 7;
            int e = (d - 1) * 8;
            if (e < 0)
            {
                e = 0;
            }
            b ^= (uint)word[d] << d;

            hash ^= Mix(b) * a;
            hash = Mix(hash);

            hash %= (uint)filter.Length;
            return (uint)hash;
        }

        // Rabin Fingerprint
        protected uint Hash8(string word)
        {
            uint hash = word[0];
            uint length = (uint)word.Length;

            for (int a = 1; a < length; a++)
            {
                hash += (uint)Math.Pow(word[a] * length, a);
            }

            hash %= (uint)filter.Length;
            return hash;
        }

        // Fletcher-32
        protected uint Hash9(string word)
        {
            uint a = 0;
            uint b = 0;

            for (int c = 0; c < word.Length; c++)
            {
                a = (a + word[c] % (uint)0xffff);
                b = (a + b) % (uint)0xffff;
            }

            uint hash = (b << 16) | a;

            hash %= (uint)filter.Length;
            return hash;
        }

        // CRC32
        protected uint Hash10(string word)
        {
            // randomly generated number
            uint hash = 0xffffffff;
            uint index;
            byte[] nums = { 114, 177, 249, 4, 222, 117, 190, 121, 130, 78, 53, 196, 255, 208, 5, 116, 221, 27, 144, 41, 252, 33, 170, 231, 62, 89, 235, 111, 174, 57, 105, 132, 204, 205, 151, 135, 90, 211, 37, 36, 66, 164, 40, 253, 108, 153, 98, 156, 67, 214, 35, 6, 38, 42, 162, 148, 28, 18, 254, 79, 61, 155, 3, 25, 184, 189, 152, 143, 84, 216, 87, 44, 75, 138, 191, 158, 243, 230, 1, 242, 91, 113, 26, 171, 245, 197, 22, 68, 187, 161, 218, 246, 97, 16, 234, 193, 73, 125, 101, 80, 226, 195, 139, 49, 9, 212, 224, 63, 72, 13, 100, 233, 104, 163, 207, 247, 137, 199, 136, 160, 203, 141, 250, 71, 200, 167, 129, 32, 19, 145, 238, 43, 142, 237, 198, 64, 76, 103, 182, 149, 2, 74, 107, 124, 88, 54, 157, 159, 51, 52, 102, 201, 7, 77, 180, 110, 109, 228, 85, 99, 11, 239, 169, 12, 8, 209, 165, 168, 248, 34, 82, 112, 140, 56, 120, 185, 55, 58, 31, 179, 47, 213, 86, 206, 194, 69, 127, 147, 123, 20, 219, 166, 29, 223, 220, 83, 70, 225, 188, 60, 21, 251, 240, 10, 119, 122, 23, 131, 96, 178, 227, 126, 173, 14, 17, 176, 192, 15, 46, 65, 215, 134, 232, 115, 106, 181, 175, 48, 202, 154, 150, 81, 50, 183, 39, 229, 92, 24, 217, 45, 172, 95, 128, 93, 133, 244, 210, 186, 118, 59, 30, 241, 146, 236, 94, 0 };

            byte[] bytes = Encoding.UTF8.GetBytes(word);

            foreach (var b in bytes)
            {
                index = ((hash ^ b) & 0xff) % (uint)nums.Length;
                hash = (hash >> 8) ^ nums[index];
            }

            hash ^= 0xffffffff;

            hash %= (uint)filter.Length;
            return hash;
        }

        // helpers
        // for hash2
        protected uint CircularShift(uint a)
        {
            uint b = a << 1 | a >> 31;
            return b;
        }

        // for hash7
        protected uint Mix(uint hash)
        {
            long a = (long)hash;
            a ^= a >> 23;
            // randomly generated number
            a *= 0x2127599bf4325c37;
            a ^= a >> 47;

            hash = (uint)a;
            return (uint)hash;
        }
    }
}