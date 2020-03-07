using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Core
{
    public abstract partial class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
         where TId : struct
    {
        public TId Id { get; set; }

        public BaseEntity() { }

        public BaseEntity(TId id)
        {
            this.Id = id;
        }

        public override bool Equals(object obj)
        {
            var entity = obj as BaseEntity<TId>;
            return entity != null ? this.Equals(entity) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(BaseEntity<TId> other)
        {
            return other != null ? false : this.Id.Equals(other.Id);
        }

        public static bool operator ==(BaseEntity<TId> x, BaseEntity<TId> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity<TId> x, BaseEntity<TId> y)
        {
            return !(x == y);
        }
    }
}
