using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids {
    public class TotalsManager : MonoBehaviour {
        private Dictionary<IMemberCollection, Dictionary<BlockContext, TotalsCache>> totalsDictionary;

        private void Awake() {
            totalsDictionary = new Dictionary<IMemberCollection, Dictionary<BlockContext, TotalsCache>>();
            S.Research.ResearchChanged += InvalidateTotals;
        }

        public GridTotals StartWatching(IMemberCollection grid, BlockContext context) {
            if (totalsDictionary.ContainsKey(grid) && totalsDictionary[grid].ContainsKey(context)) {
                throw new Exception("Grid with given context already being watched");
            }

            var cache = new TotalsCache(grid) {
                totals = new GridTotals(context)
            };

            cache.Reset();
            cache.AddListeners();

            totalsDictionary.EnsureCreated(
                grid,
                () => new Dictionary<BlockContext, TotalsCache> {
                    {
                        context, cache
                    }
                }
            );

            return cache.totals;
        }

        public GridTotals GetGridTotalsSafe(IMemberCollection grid, BlockContext context) {
            return totalsDictionary.Get(grid)?.Get(context) == null
                ? StartWatching(grid, context)
                : GetGridTotals(grid, context);
        }

        public GridTotals GetGridTotals(IMemberCollection grid, BlockContext context) {
            return totalsDictionary[grid][context].totals;
        }

        public void StopWatching(IMemberCollection grid, BlockContext context) {
            totalsDictionary[grid][context].RemoveListeners();
            totalsDictionary[grid].Remove(context);

            if (totalsDictionary[grid].Values.Count == 0) {
                totalsDictionary.Remove(grid);
            }
        }

        private void InvalidateTotals(BlockContext context) {
            IEnumerable<TotalsCache> GetInvalidCaches() {
                foreach (var (cacheContext, cache) in totalsDictionary
                    .SelectMany(blockContextDict => blockContextDict.Value.Unpack())
                ) {
                    if (cacheContext == context) {
                        yield return cache;
                    }
                }
            }

            foreach (var cache in GetInvalidCaches()) {
                cache.Reset();
            }
        }

        private class TotalsCache : MemberCollectionListener<IMemberCollection> {
            public GridTotals totals;

            public TotalsCache(IMemberCollection source) : base(source) { }

            public override void Reset() {
                totals.Reset();
                base.Reset();
            }

            protected override void OnMemberAdded(IGridMember member) {
                member.AddGridTotals(totals);
            }

            protected override void OnMemberRemoved(IGridMember member) {
                member.RemoveGridTotals(totals);
            }
        }
    }
}