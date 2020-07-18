using System;
using System.Collections.Generic;
using Moonlight.Game.Entities;
using NosCore.Packets.Enumerations;

namespace Moonlight.Utility
{
    public static class EntityUtility
    {
        private static readonly Dictionary<Type, VisualType> _typeMapping;

        static EntityUtility() =>
            _typeMapping = new Dictionary<Type, VisualType>
            {
                [typeof(Monster)] = VisualType.Monster,
                [typeof(Npc)] = VisualType.Npc,
                [typeof(Player)] = VisualType.Player,
                [typeof(GroundItem)] = VisualType.Object
            };

        public static VisualType GetVisualType<T>() where T : Entity => _typeMapping.GetValueOrDefault(typeof(T));
    }
}