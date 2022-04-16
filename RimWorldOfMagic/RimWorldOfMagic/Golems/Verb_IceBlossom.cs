﻿using Verse;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RimWorld;
using TorannMagic.Golems;

namespace TorannMagic.Golems
{
    public class Verb_IceBlossom : Verb_GolemShoot
    {
        protected override bool TryCastShot()
        {
            LocalTargetInfo tmpTarget = null;
            Pawn p = currentTarget.Pawn;
            Thing launchedThing = new Thing()
            {
                def = this.verbProps.defaultProjectile
            };
            if (this.verbProps.minRange != 0 && p != null)
            {
                List<Pawn> tmpPawns = TM_Calc.FindPawnsNearTarget(p, Mathf.RoundToInt(this.verbProps.minRange), this.currentTarget.Cell, true);
                if (tmpPawns != null)
                {
                    tmpTarget = new LocalTargetInfo(tmpPawns.RandomElement());
                }
            }
            for (int i = 0; i < 4; i++)
            {
                tmpTarget = currentTarget;
                if (!Rand.Chance(this.verbProps.accuracyLong))
                {
                    tmpTarget = TM_Calc.FindValidCellWithinRange(currentTarget.Cell, CasterPawn.Map, 1);
                }
                FlyingObject_Advanced flyingObject = (FlyingObject_Advanced)GenSpawn.Spawn(this.verbProps.defaultProjectile, CasterPawn.Position, CasterPawn.Map);
                flyingObject.AdvancedLaunch(CasterPawn, TorannMagicDefOf.Mote_Ice, 3, Mathf.Clamp(Rand.Range(2, 60),
                    0, tmpTarget.Cell.DistanceToEdge(CasterPawn.Map)), false, CasterPawn.DrawPos, tmpTarget, launchedThing, Rand.Range(40, 60), this.verbProps.defaultProjectile.projectile.explosionRadius > 0, Rand.Range(12, 16),
                    this.verbProps.defaultProjectile.projectile.explosionRadius, this.verbProps.defaultProjectile.projectile.damageDef, null, 0, this.verbProps.defaultProjectile.projectile.flyOverhead, .4f);
            }
            return base.TryCastShot();
        }
    }
}
