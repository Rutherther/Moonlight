using System;
using System.Collections.Generic;
using System.Linq;
using Moonlight.Core;
using Moonlight.Core.Collection;
using Moonlight.Game.Entities;
using Moonlight.Utility;
using NosCore.Packets.Enumerations;

namespace Moonlight.Game.Maps
{
    public class Map
    {
        internal Map(int id, string name, byte[] grid)
        {
            Id = id;
            Name = name;
            Grid = grid;
            Width = BitConverter.ToInt16(Grid.Take(2).ToArray(), 0);
            Height = BitConverter.ToInt16(Grid.Skip(2).Take(2).ToArray(), 0);

            Monsters = new InternalObservableDictionary<long, Monster>();
            Npcs = new InternalObservableDictionary<long, Npc>();
            GroundItems = new InternalObservableDictionary<long, GroundItem>();
            Players = new InternalObservableDictionary<long, Player>();
            Portals = new InternalObservableDictionary<long, Portal>();
        }

        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();

        public int Id { get; }
        public string Name { get; }
        public byte[] Grid { get; }
        public short Width { get; }
        public short Height { get; }

        public InternalObservableDictionary<long, Monster> Monsters { get; }
        public InternalObservableDictionary<long, Npc> Npcs { get; }
        public InternalObservableDictionary<long, Player> Players { get; }
        public InternalObservableDictionary<long, GroundItem> GroundItems { get; }
        public InternalObservableDictionary<long, Portal> Portals { get; }
        public IEnumerable<Entity> Entities => Monsters.Concat(Npcs.Cast<Entity>()).Concat(Players).Concat(GroundItems);

        public Entity GetEntity(VisualType entityType, long entityId)
        {
            switch (entityType)
            {
                case VisualType.Npc:
                    return Npcs.GetValueOrDefault(entityId);
                case VisualType.Monster:
                    return Monsters.GetValueOrDefault(entityId);
                case VisualType.Player:
                    return Players.GetValueOrDefault(entityId);
                case VisualType.Object:
                    return GroundItems.GetValueOrDefault(entityId);
                default:
                    throw new InvalidOperationException();
            }
        }

        public T GetEntity<T>(VisualType entityType, long entityId) where T : Entity
        {
            Entity entity = GetEntity(entityType, entityId);
            if (!(entity is T castEntity))
            {
                return default;
            }

            return castEntity;
        }

        public T GetEntity<T>(long entityId) where T : Entity
        {
            VisualType entityType = EntityUtility.GetVisualType<T>();
            return GetEntity<T>(entityType, entityId);
        }

        public Portal GetPortal(int id) => Portals.GetValueOrDefault(id);

        public bool Contains(VisualType entityType, long entityId) => GetEntity(entityType, entityId) != null;

        public IEnumerable<Entity> GetEntities(VisualType entityType)
        {
            switch (entityType)
            {
                case VisualType.Npc:
                    return Npcs;
                case VisualType.Player:
                    return Players;
                case VisualType.Monster:
                    return Monsters;
                case VisualType.Object:
                    return GroundItems;
                default:
                    return Array.Empty<Entity>();
            }
        }

        public IEnumerable<T> GetEntities<T>() where T : Entity
        {
            VisualType entityType = EntityUtility.GetVisualType<T>();
            return GetEntities(entityType).Cast<T>();
        }

        internal void AddPortal(Portal portal)
        {
            Portals[portal.Id] = portal;
        }

        internal void AddEntity(Entity entity)
        {
            switch (entity.VisualType)
            {
                case VisualType.Npc:
                    Npcs[entity.Id] = (Npc)entity;
                    break;
                case VisualType.Monster:
                    Monsters[entity.Id] = (Monster)entity;
                    break;
                case VisualType.Player:
                    Players[entity.Id] = (Player)entity;
                    break;
                case VisualType.Object:
                    GroundItems[entity.Id] = (GroundItem)entity;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            entity.Map = this;
        }

        internal void RemoveEntity(Entity entity)
        {
            RemoveEntity(entity.VisualType, entity.Id);
        }

        internal void RemoveEntity(VisualType entityType, long entityId)
        {
            switch (entityType)
            {
                case VisualType.Npc:
                    Npcs.Remove(entityId);
                    break;
                case VisualType.Monster:
                    Monsters.Remove(entityId);
                    break;
                case VisualType.Player:
                    Players.Remove(entityId);
                    break;
                case VisualType.Object:
                    GroundItems.Remove(entityId);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public bool IsWalkable(Position position)
        {
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                return false;
            }

            byte value = this[position.X, position.Y];

            return value == 0 || value == 2 || value >= 16 && value <= 19;
        }
    }
}