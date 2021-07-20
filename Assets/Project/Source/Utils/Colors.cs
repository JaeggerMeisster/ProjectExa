using UnityEngine;

namespace Exa.Utils {
    public static class Colors {
        public static Color White {
            get => S.Colors.white;
        }

        public static Color DimGray {
            get => S.Colors.dimGray;
        }

        public static Color Onyx {
            get => S.Colors.onyx;
        }

        public static Color RaisinBlack {
            get => S.Colors.raisinBlack;
        }

        public static Color Verdigris {
            get => S.Colors.verdigris;
        }

        public static Color RoseVale {
            get => S.Colors.roseVale;
        }

        public static Color GetActiveColor(bool active) {
            return active ? Verdigris : RoseVale;
        }
    }
}