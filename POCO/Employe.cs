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
    public partial class Employe : Utilisateur
    {
        #region Navigation Properties
    
        public virtual ICollection<Article> Articles
        {
            get
            {
                if (_articles == null)
                {
                    var newCollection = new FixupCollection<Article>();
                    newCollection.CollectionChanged += FixupArticles;
                    _articles = newCollection;
                }
                return _articles;
            }
            set
            {
                if (!ReferenceEquals(_articles, value))
                {
                    var previousValue = _articles as FixupCollection<Article>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupArticles;
                    }
                    _articles = value;
                    var newValue = value as FixupCollection<Article>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupArticles;
                    }
                }
            }
        }
        private ICollection<Article> _articles;
    
        public virtual ICollection<Session> Sessions
        {
            get
            {
                if (_sessions == null)
                {
                    var newCollection = new FixupCollection<Session>();
                    newCollection.CollectionChanged += FixupSessions;
                    _sessions = newCollection;
                }
                return _sessions;
            }
            set
            {
                if (!ReferenceEquals(_sessions, value))
                {
                    var previousValue = _sessions as FixupCollection<Session>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupSessions;
                    }
                    _sessions = value;
                    var newValue = value as FixupCollection<Session>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupSessions;
                    }
                }
            }
        }
        private ICollection<Session> _sessions;
    
        public virtual ICollection<Article> Articles_1
        {
            get
            {
                if (_articles_1 == null)
                {
                    var newCollection = new FixupCollection<Article>();
                    newCollection.CollectionChanged += FixupArticles_1;
                    _articles_1 = newCollection;
                }
                return _articles_1;
            }
            set
            {
                if (!ReferenceEquals(_articles_1, value))
                {
                    var previousValue = _articles_1 as FixupCollection<Article>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupArticles_1;
                    }
                    _articles_1 = value;
                    var newValue = value as FixupCollection<Article>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupArticles_1;
                    }
                }
            }
        }
        private ICollection<Article> _articles_1;

        #endregion
        #region Association Fixup
    
        private void FixupArticles(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Article item in e.NewItems)
                {
                    item.Employe = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Article item in e.OldItems)
                {
                    if (ReferenceEquals(item.Employe, this))
                    {
                        item.Employe = null;
                    }
                }
            }
        }
    
        private void FixupSessions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Session item in e.NewItems)
                {
                    item.Employe = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Session item in e.OldItems)
                {
                    if (ReferenceEquals(item.Employe, this))
                    {
                        item.Employe = null;
                    }
                }
            }
        }
    
        private void FixupArticles_1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Article item in e.NewItems)
                {
                    if (!item.Utilisateurs_Employe.Contains(this))
                    {
                        item.Utilisateurs_Employe.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Article item in e.OldItems)
                {
                    if (item.Utilisateurs_Employe.Contains(this))
                    {
                        item.Utilisateurs_Employe.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}
