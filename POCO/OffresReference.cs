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
    public partial class OffresReference
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
    
        public virtual float Prix
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Offre> Offres
        {
            get
            {
                if (_offres == null)
                {
                    var newCollection = new FixupCollection<Offre>();
                    newCollection.CollectionChanged += FixupOffres;
                    _offres = newCollection;
                }
                return _offres;
            }
            set
            {
                if (!ReferenceEquals(_offres, value))
                {
                    var previousValue = _offres as FixupCollection<Offre>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOffres;
                    }
                    _offres = value;
                    var newValue = value as FixupCollection<Offre>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOffres;
                    }
                }
            }
        }
        private ICollection<Offre> _offres;
    
        public virtual ICollection<OffresReferencesTypeSession> OffresReferencesTypeSessions
        {
            get
            {
                if (_offresReferencesTypeSessions == null)
                {
                    var newCollection = new FixupCollection<OffresReferencesTypeSession>();
                    newCollection.CollectionChanged += FixupOffresReferencesTypeSessions;
                    _offresReferencesTypeSessions = newCollection;
                }
                return _offresReferencesTypeSessions;
            }
            set
            {
                if (!ReferenceEquals(_offresReferencesTypeSessions, value))
                {
                    var previousValue = _offresReferencesTypeSessions as FixupCollection<OffresReferencesTypeSession>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOffresReferencesTypeSessions;
                    }
                    _offresReferencesTypeSessions = value;
                    var newValue = value as FixupCollection<OffresReferencesTypeSession>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOffresReferencesTypeSessions;
                    }
                }
            }
        }
        private ICollection<OffresReferencesTypeSession> _offresReferencesTypeSessions;

        #endregion
        #region Association Fixup
    
        private void FixupOffres(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Offre item in e.NewItems)
                {
                    item.OffresReference = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Offre item in e.OldItems)
                {
                    if (ReferenceEquals(item.OffresReference, this))
                    {
                        item.OffresReference = null;
                    }
                }
            }
        }
    
        private void FixupOffresReferencesTypeSessions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (OffresReferencesTypeSession item in e.NewItems)
                {
                    item.OffresReference = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OffresReferencesTypeSession item in e.OldItems)
                {
                    if (ReferenceEquals(item.OffresReference, this))
                    {
                        item.OffresReference = null;
                    }
                }
            }
        }

        #endregion
    }
}
