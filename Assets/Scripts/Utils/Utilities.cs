using UnityEngine;
using Game.Gameplay.Item;
using System.Collections.Generic;
using Game.Gameplay.Board;

namespace Utils {
    public static class Utilities {
        public static bool AreVerticalOrHorizontalNeighbors(Item a, Item b) {
            return (a.Column == b.Column || a.Row == b.Row)
                            && Mathf.Abs(a.Column - b.Column) <= 1
                            && Mathf.Abs(a.Row - b.Row) <= 1;
        }

        public static void FixSortingLayer(SpriteRenderer sr1, SpriteRenderer sr2) {
            if (sr1.sortingOrder <= sr2.sortingOrder) {
                sr1.sortingOrder = 1;
                sr2.sortingOrder = 0;
            }
        }

        public static void SwapItemData(Item a, Item b) {
            int temp = a.Row;
            a.Row = b.Row;
            b.Row = temp;

            temp = a.Column;
            a.Column = b.Column;
            b.Column = temp;
        }

        public static IEnumerable<T> SliceArray<T>(this T[,] array, int index) {
            if (PlayerInfo.Player.Instance.IsRowMatch) {
                for (int i = 0; i < array.GetLength(1); i++) {
                    yield return array[index, i];
                }
            }

            else {
                for (int i = 0; i < array.GetLength(0); i++) {
                    yield return array[i, index];
                }
            }
        }
    }
}

