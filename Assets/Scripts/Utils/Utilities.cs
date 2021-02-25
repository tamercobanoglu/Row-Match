using Game.Gameplay.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public static void SwapColumnRow(Item a, Item b) {
            int temp = a.Row;
            a.Row = b.Row;
            b.Row = temp;

            temp = a.Column;
            a.Column = b.Column;
            b.Column = temp;
        }
    }
}

