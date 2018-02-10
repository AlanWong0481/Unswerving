//1.01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gibgibLibrary {
    /// <summary>
    /// 數學計算
    /// </summary>
    public static class math {
        /// <summary>
        /// 百分比計算
        /// </summary>
        public static float zoomNumber(float number,int zoomPercentage) {
            return (number / 100) * zoomPercentage;
        }

        /// <summary>
        /// 上捨入法
        /// </summary>
        public static int RoundUp(float number) {
            return (int)(number - (number % 1))+1;
        }

        /// <summary>
        /// 下捨入法
        /// </summary>
        public static int RoundDown(float number) {
            return (int)(number - (number % 1) );
        }

    }

    /// <summary>
    /// array處理與管理
    /// </summary>
    public static class arrayManagement {

        /// <summary>
        /// 加總array所有數值
        /// </summary>
        public static int totalNumberArray(int[] intArray) {
            int outPutData = 0;
            foreach (var item in intArray) {
                outPutData += item;
            }
            return outPutData;
        }
        /// <summary>
        /// 加總array所有數值
        /// </summary>
        public static float totalNumberArray(float[] intArray) {
            float outPutData = 0;
            foreach (var item in intArray) {
                outPutData += item;
            }
            return outPutData;
        }


        /// <summary>
        /// 找出這個array最大的index
        /// </summary>
        public static int findBiggestIndexInArray(int[] intArray) {
            int outPutData = 0;

            for (int i = 0; i < intArray.Length; i++) {
                if (outPutData == i) {
                    continue;
                }
                if (intArray[outPutData] < i) {
                    outPutData = i;
                }
            }

            return outPutData;
        }
        /// <summary>
        /// 找出這個array最細的index
        /// </summary>
        public static int findSmallesttIndexInArray(int[] intArray) {
            int outPutData = 0;
            for (int i = 0; i < intArray.Length; i++) {
                if (outPutData == i) {
                    continue;
                }
                if (intArray[ outPutData ] > i) {
                    outPutData = i;
                }
            }

            return outPutData;
        }

        /// <summary>
        /// 找出這個array第幾大的index
        /// </summary>
        public static int findTheFewBigIndexInArray(int[] intArray,int TheFirstFewBig) {
            if (TheFirstFewBig > intArray.Length || TheFirstFewBig <= 0) {
                Debug.LogError("TheFirstFewBig cant be less than 0 and big more array length");
                return 0;
            }

            int[] BiggestArray = new int[ TheFirstFewBig ];
            bool find = false;
            for (int j = 0; j < BiggestArray.Length; j++) {

                for (int i = 0; i < intArray.Length; i++) {
                    if (BiggestArray[ j] == i) {
                        continue;
                    }
                    for (int k = 0; k < j; k++) {
                        if (BiggestArray[ k ] == i) {
                            find = true;
                            break;
                        }
                    }
                    if (find) {
                        find = false;
                        continue;
                    }
                    if (intArray[ BiggestArray[ j] ] < i) {
                        BiggestArray[ j] = i;
                    }
                }
            }


            return BiggestArray[ TheFirstFewBig-1];
        }
        /// <summary>
        /// 找出這個array第幾細的index
        /// </summary>
        public static int findTheFewSmallIndexInArray(int[] intArray, int TheFirstFewSmall) {
            if (TheFirstFewSmall > intArray.Length || TheFirstFewSmall <= 0) {
                Debug.LogError("TheFirstFewSmall cant be less than 0 and big more array length");
                return 0;
            }

            int[] SmallestArray = new int[ TheFirstFewSmall ];
            bool find = false;
            for (int j = 0; j < SmallestArray.Length; j++) {

                for (int i = 0; i < intArray.Length; i++) {
                    if (SmallestArray[ j ] == i) {
                        continue;
                    }
                    for (int k = 0; k < j; k++) {
                        if (SmallestArray[ k ] == i) {
                            find = true;
                            break;
                        }
                    }
                    if (find) {
                        find = false;
                        continue;
                    }
                    if (intArray[ SmallestArray[ j ] ] > i) {
                        SmallestArray[ j ] = i;
                    }
                }
            }


            return SmallestArray[ TheFirstFewSmall - 1 ];
        }

        /// <summary>
        /// 找出這個boolarray有沒有其中一值是true
        /// </summary>
        public static bool detectBoolArrayHaveAnyTrue(bool[] boolArray) {
            bool outPutData = false;
            foreach (var item in boolArray) {
                if (item) {
                    outPutData = true;
                    return outPutData;
                }
            }
            return outPutData;
        }
        /// <summary>
        /// 找出這個boolarray有沒有其中一值是false
        /// </summary>
        public static bool detectBoolArrayHaveAnyFalse(bool[] boolArray) {
            bool outPutData = false;
            foreach (var item in boolArray) {
                if (!item) {
                    outPutData = true;
                    return outPutData;
                }
            }
            return outPutData;
        }

        /// <summary>
        /// 隨機輸出index
        /// </summary>
        public static int randomPickUpIndex<T>(T[] intArray) {
            return Random.Range(0,intArray.Length);
        }

        /// <summary>
        /// 簡易排列
        /// </summary>
        public static int[] SortingArray(int[] intArray,bool isBigestToSmallest) {
            int[] outPutData = intArray;
            int saveNumber = 0;
            for (int i = 0; i < outPutData.Length; i++) {
                for (int j = 0; j < outPutData.Length-1; j++) {

                    if (isBigestToSmallest) {
                        if (outPutData[ j ] < outPutData[ j + 1 ]) {
                            saveNumber =  outPutData[ j ] ;
                            outPutData[ j ] = outPutData[ j + 1 ];
                            outPutData[ j + 1 ] = saveNumber;
                        }
                    } else {
                        if (outPutData[ j ] > outPutData[ j + 1 ]) {
                            saveNumber = outPutData[ j ];
                            outPutData[ j ] = outPutData[ j + 1 ];
                            outPutData[ j + 1 ] = saveNumber;
                        }
                    }
                }
            }
            return outPutData;
        }

        /// <summary>
        /// </summary>
        public static T[] Displacement<T>(T[] array, int moveHowMuch,bool isLeft) {
            var arrayItem = array[0];
            for (int i = 0; i < moveHowMuch; i++) {
                if (isLeft) {
                    for (int j = 1; j < array.Length; j++) {
                        arrayItem = array[ j - 1 ];
                        array[ j - 1 ] = array[ j ];
                        array[ j ] = arrayItem;
                    }
                } else {
                    for (int j = array.Length-2; j >= 0; j--) {
                        arrayItem = array[ j + 1 ];
                        array[ j + 1 ] = array[ j ];
                        array[ j ] = arrayItem;
                    }
                }
  
            }


            return array;
        }

        /// <summary>
        /// 移除指定index
        /// </summary>
        public static T[] removeIndex<T>(T[] intArray,int index) {
            T[] outPutData = new T[ intArray.Length -1 ];
            int j = 0;
            for (int i = 0; i < intArray.Length; i++) {
                if (i == index) {
                    continue;
                }
                outPutData[ j ] = intArray[ i ];
                j++;
            }
            return outPutData;
        }
        
        /// <summary>
        /// 以文本方式輸出arrayData
        /// </summary>
        public static string viewArrayData<T>(T[] intArray) {
            string outPutData = "";
            for (int i = 0; i < intArray.Length-1; i++) {
                outPutData += intArray[i] + ",";

            }
            outPutData += intArray[ intArray.Length-1 ];
            return outPutData;
        }

    }

    /*
    public static class testZone {
        public static void test<T>(T intone) {

        }
        public static T test2<T>(T intone) {
            T outputData;
            return intone;
        }
    }
    */
}


