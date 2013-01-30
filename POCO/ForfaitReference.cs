//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace POCO
{
    public partial class ForfaitReference
    {
        #region Primitive Properties
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string Nom
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<OffresReference> OffresReferences
        {
            get
            {
                if (_offresReferences == null)
                {
                    var newCollection = new FixupCollection<OffresReference>();
                    newCollection.CollectionChanged += FixupOffresReferences;
                    _offresReferences = newCollection;
                }
                return _offresReferences;
            }
            set
            {
                if (!ReferenceEquals(_offresReferences, value))
                {
                    var previousValue = _offresReferences as FixupCollection<OffresReference>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOffresReferences;
                    }
                    _offresReferences = value;
                    var newValue = value as FixupCollection<OffresReference>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOffresReferences;
                    }
                }
            }
        }
        private ICollection<OffresReference> _offresReferences;

        #endregion
        #region Association Fixup
    
        private void FixupOffresReferences(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (OffresReference item in e.NewItems)
                {
                    item.ForfaitReference = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OffresReference item in e.OldItems)
                {
                    if (ReferenceEquals(item.ForfaitReference, this))
                    {
                        item.ForfaitReference = null;
                    }
                }
            }
        }

        #endregion
    }
}
