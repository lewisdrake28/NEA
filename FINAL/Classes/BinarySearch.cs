// // install dependencies
// using System.Collections;

// // suppress warnings
// #pragma warning disable 

// namespace BinarySearch
// {
//     class BinarySearch
//     {
//         protected int Search(string[] items, string target)
//         {
//             double left = 0;
//             double right = items.Length - 1;
//             double centre;

//             while (left != right)
//             {
//                 centre = (left + right) / 2;
//                 centre = Math.Ceiling(centre);

//                 if (items[centre] > target)
//                 {
//                     right = centre - 1;
//                 }
//             }
//         }
//     }
// }