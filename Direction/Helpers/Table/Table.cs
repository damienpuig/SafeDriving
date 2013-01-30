using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Reflection;
using Direction.Helpers;
using System.Text;

namespace Direction.Helpers.Table
{
    public class Table<TModel, T> : IDisposable
    {
        #region Properties

        private HtmlHelper<TModel> TableHtmlHelper { get; set; }
        private IList<T> ObjectList { get; set; }
        private List<TableColumn<T>> Columns { get; set; }
        public string TableId { get; set; }
        public string TableTitle { get; set; }

        #endregion

        #region Construtor

        public Table(HtmlHelper<TModel> htmlHelper, IList<T> objectData, string tableId, string tableTitle)
        {
            TableHtmlHelper = htmlHelper;
            ObjectList = objectData;
            Columns = new List<TableColumn<T>>();
            TableId = tableId;
            TableTitle = tableTitle;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Ajout d'une colonne à la table
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Table<TModel, T> AddColumns<TProperty>(Expression<Func<T, TProperty>> expression, string uniqColumnName)
        {
            return AddColumns<TProperty>(expression, uniqColumnName, null);
        }

        /// <summary>
        /// Ajout d'une colonne à la table
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Table<TModel, T> AddColumns<TProperty>(Expression<Func<T, TProperty>> expression, string uniqColumnName, object htmlOptions)
        {
            // Check du column name, celui ci doit être obligatoire et unique
            if (string.IsNullOrWhiteSpace(uniqColumnName))
            {
                throw new Exception("Le paramètre uniqColumnName est obligatoire");
            }

            var TViewModel = new ViewDataDictionary<T>();
            var metaData = ModelMetadata.FromLambdaExpression<T, TProperty>(expression, TViewModel);
            string displayName = metaData.DisplayName ?? (metaData.PropertyName ?? ExpressionHelper.GetExpressionText(expression));

            TableColumn<T> col = new TableColumn<T>();
            col.ColumnLabel = displayName;
            col.ColumnPropertyName = metaData.PropertyName;
            col.ColumnUniqName = uniqColumnName;
            col.ColumnHtmlOptions = htmlOptions;
            col.IsAction = false;
            Columns.Add(col);

            return this;
        }

        /// <summary>
        /// Ajout d'une colonne action à la table avec un lien ouvrant une page en mode popup
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Table<TModel, T> AddActionPopupLink(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression)
        {
            return AddActionPopupLink(actionColumnName, uniqColumnName, expression, null);
        }

        /// <summary>
        /// Ajout d'une colonne action à la table avec un lien ouvrant une page en mode popup
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Table<TModel, T> AddActionPopupLink(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression, object htmlOptions)
        {
            return AddAction(actionColumnName, uniqColumnName, expression, ActionType.Action.OpenPopup, htmlOptions, null, null, null);
        }

        /// <summary>
        /// Ajout d'une colonne action à la table avec un lien hyperlink
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Table<TModel, T> AddActionLink(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression)
        {
            return AddActionLink(actionColumnName, uniqColumnName, expression, null);
        }

        /// <summary>
        /// Ajout d'une colonne action à la table avec un lien hyperlink
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Table<TModel, T> AddActionLink(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression, object htmlOptions)
        {
            return AddAction(actionColumnName, uniqColumnName, expression, ActionType.Action.HyperLink, htmlOptions, null, null, null);
        }

        /// <summary>
        /// Ajout d'une colonne qui affiche un lien qui sera exécuté en ajax post
        /// </summary>
        /// <param name="actionColumnName"></param>
        /// <param name="uniqColumnName"></param>
        /// <param name="expression"></param>
        /// <param name="htmlOptions"></param>
        /// <returns></returns>
        public Table<TModel, T> AddAjaxActionLink(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression, object htmlOptions, string label)
        {
            return AddAction(actionColumnName, uniqColumnName, expression, ActionType.Action.HyperLinkAction, htmlOptions, label, null, null);
        }

        /// <summary>
        ///  Ajout d'une colonne Checkbox à la table
        /// </summary>
        /// <param name="actionColumnName">Le nom de la colonne destinée à l'affichage</param>
        /// <param name="uniqColumnName">Le nom unique de la colonne (N'ai pas utilisée lors de l'affichage)</param>
        /// <param name="propertyPostee">La propriété dans laquelle sera postée les ids des lignes checkées</param>
        /// <param name="propertyId">La property qui permet de construire l'id</param>
        /// <returns></returns>
        public Table<TModel, T> AddMultiselectionCheckBoxColumn(string actionColumnName, string uniqColumnName, Expression<Func<TModel, object>> propertyPosteeExpression, Expression<Func<T, object>> propertyIdExpression)
        {
            string hiddenInputForCheckboxId = ModelHelper.GetPropertyName(propertyPosteeExpression);
            string lineIdentifier = ModelHelper.GetPropertyName(propertyIdExpression);
            return AddAction(actionColumnName, uniqColumnName, propertyIdExpression, ActionType.Action.CheckBox, null, lineIdentifier, hiddenInputForCheckboxId, null);
        }

        /// <summary>
        /// Ajout d'une colonne Checkbox action à la table
        /// </summary>
        /// <param name="actionColumnName">Le nom affiché de la colonne</param>
        /// <param name="uniqColumnName">Le nom système unique de la colonne</param>
        /// <param name="expression">L'expression de l'action à executer</param>
        /// <param name="propertyValueExpression">L'expression permettant de déterminer l'id de la ligne</param>
        /// <param name="htmlOptions"></param>
        /// <returns></returns>
        public Table<TModel, T> AddCheckBoxActionColumn(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression, Expression<Func<T, object>> propertyValueExpression, object htmlOptions)
        {
            return AddAction(actionColumnName, uniqColumnName, expression, ActionType.Action.CheckBoxAction, htmlOptions, null, null, propertyValueExpression);
        }

        public MvcHtmlString Render()
        {
            // Thead
            TagBuilder thead = RenderTHead();

            // Tbody
            TagBuilder tbody = RenderTBody();

            // Table
            TagBuilder table = RenderTable(thead, tbody);

            return MvcHtmlString.Create(RenderTableTitleEncapsuler(table).ToString());
        }

        public void Dispose()
        {
            Render();
        }

        #endregion

        #region Private Methods

        private Table<TModel, T> AddAction(string actionColumnName, string uniqColumnName, Expression<Func<T, object>> expression, ActionType.Action actionType, object htmlOptions, string lineIdentifier, string hiddenInputId, Expression<Func<T, object>> isCheckedExpression)
        {
            TableActionLink<T> action = new TableActionLink<T>();
            action.ActionName = actionColumnName;
            action.ActionExpression = expression;
            action.ActionType = actionType;
            action.ActionIdentifier = lineIdentifier;
            action.HiddenInputId = hiddenInputId;
            action.IsCheckedExpression = isCheckedExpression;

            TableColumn<T> col = new TableColumn<T>();
            col.ColumnLabel = actionColumnName;
            col.ColumnUniqName = uniqColumnName;
            col.ColumnHtmlOptions = htmlOptions;
            col.IsAction = true;
            col.Action = action;
            Columns.Add(col);
            return this;
        }

        /// <summary>
        /// Rendu des header de colonne de la table
        /// </summary>
        /// <returns>Le tagbuilder tHead</returns>
        private TagBuilder RenderTHead()
        {
            TagBuilder theadtr = new TagBuilder("tr");
            TagBuilder th;
            foreach (TableColumn<T> column in Columns)
            {
                th = new TagBuilder("th") { InnerHtml = column.ColumnLabel };

                if (column.ColumnHtmlOptions != null)
                {
                    InjectHTMLOptions(ref th, column.ColumnHtmlOptions);
                }

                theadtr.InnerHtml = String.Format("{0}\n{1}", theadtr.InnerHtml, th.ToString());
            }

            TagBuilder thead = new TagBuilder("thead") { InnerHtml = theadtr.ToString() };

            return thead;
        }

        /// <summary>
        /// Injection des Html options définies sur une colonne ou une action
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="htmlOptions"></param>
        private void InjectHTMLOptions(ref TagBuilder tag, object htmlOptions)
        {
            if (htmlOptions != null)
            {
                // Extract properties from htmlOptions
                PropertyInfo[] properties = htmlOptions.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    tag.MergeAttribute(property.Name, property.GetValue(htmlOptions, null).ToString());
                }
            }
        }

        /// <summary>
        /// Rendu du tBody de la table
        /// </summary>
        /// <returns></returns>
        private TagBuilder RenderTBody()
        {
            TagBuilder tbody = new TagBuilder("tbody");
            StringBuilder tbodyBuilder = new StringBuilder();
            foreach (object o in ObjectList)
            {
                TagBuilder tbodytr = new TagBuilder("tr");

                // Construction de chacune des lignes de data
                InjectContent(ref tbodytr, o);
                tbodyBuilder.Append("\n");
                tbodyBuilder.Append(tbodytr);
            }
            tbody.InnerHtml = tbodyBuilder.ToString();

            return tbody;
        }

        /// <summary>
        /// Rendu de la table compléte en injectant le thead et le tBody
        /// </summary>
        /// <param name="thead">Le header de la table</param>
        /// <param name="tbody">Le body de la table</param>
        /// <returns>Un TagBuilder Table complet</returns>
        private TagBuilder RenderTable(TagBuilder thead, TagBuilder tbody)
        {
            TagBuilder table = new TagBuilder("table") { InnerHtml = String.Format("{0}\n{1}", thead.ToString(), tbody.ToString()) };
            table.MergeAttribute("id", string.IsNullOrEmpty(TableId) ? String.Format("table-{0}", Columns.Count.ToString()) : TableId);
            table.MergeAttribute("controltype", "Grid");
            table.MergeAttribute("class", "display");

            return table;
        }

        private TagBuilder RenderTableTitleEncapsuler(TagBuilder table)
        {
            if (string.IsNullOrWhiteSpace(TableTitle) == false)
            {
                TagBuilder Container = new TagBuilder("div");
                Container.MergeAttribute("class", "GridContainer");

                TagBuilder TitleContainer = new TagBuilder("div");
                TitleContainer.InnerHtml = TableTitle;
                TitleContainer.MergeAttribute("class", "GridTitleContainer");

                TagBuilder TableContainer = new TagBuilder("div");
                TableContainer.InnerHtml = table.ToString();
                TableContainer.MergeAttribute("class", "GridTableContainer");

                // Ajout du contenu au container
                Container.InnerHtml = TitleContainer.ToString() + TableContainer.ToString();

                return Container;
            }

            return table;
        }


        /// <summary>
        /// Ajout du contenu de l'objet dans sa ligne d'affichage
        /// </summary>
        /// <param name="tbodytr">Le tr (ligne)</param>
        /// <param name="objectDataProperties">La liste des propriétés de l'objet (Bindé sur la liste des colonnes à afficher)</param>
        /// <param name="objetAAfficher">L'objet affiché dans la ligne</param>
        private void InjectContent(ref TagBuilder tbodytr, object objetAAfficher)
        {
            foreach (TableColumn<T> column in Columns)
            {
                if (column.IsAction == false)
                    RenderContent(ref tbodytr, objetAAfficher, column.ColumnPropertyName, column.ColumnHtmlOptions);
                else if (column.IsAction == true && column.Action != null)
                    RenderContent(ref tbodytr, objetAAfficher, column.Action, column.ColumnHtmlOptions);
                else
                    throw new Exception("Un item invalide veut se rendre...");
            }
        }

        /// <summary>
        /// Ajout du contenu de l'objet dans sa ligne d'affichage
        /// </summary>
        /// <param name="tbodytr">Le tr (ligne)</param>
        /// <param name="objectDataProperties">La liste des propriétés de l'objet (Bindé sur la liste des colonnes à afficher)</param>
        /// <param name="objetAAfficher">L'objet affiché dans la ligne</param>
        private void RenderContent(ref TagBuilder tbodytr, object objetAAfficher, string property, object htmloptions)
        {
            string val = String.Empty;
            object pval = ModelHelper.GetPropertyValue(property.ToString(), objetAAfficher);
            if (pval != null)
                val = pval.ToString();

            TagBuilder td = new TagBuilder("td") { InnerHtml = val };
            InjectHTMLOptions(ref td, htmloptions);
            tbodytr.InnerHtml = String.Format("{0}\n{1}", tbodytr.InnerHtml, td.ToString());
        }

        /// <summary>
        /// Ajout des actions dans le TR (TagBuilder) passé en référence
        /// </summary>
        /// <param name="tbodytr">Le tr (ligne)</param>
        /// <param name="objetAAfficher">L'objet affiché dans la ligne</param>
        private void RenderContent(ref TagBuilder tbodytr, object objetAAfficher, ITableAction<T> action, object htmloptions)
        {
            TagBuilder Td = new TagBuilder("td");
            string val = string.Empty;

            if (action.ActionType == ActionType.Action.CheckBox)
            {
                object pval = ModelHelper.GetPropertyValue(action.ActionIdentifier, objetAAfficher);
                string lineIdentifier = string.Empty;
                if (pval != null)
                    lineIdentifier = pval.ToString();

                TagBuilder chk = new TagBuilder("input");
                chk.MergeAttribute("value", lineIdentifier);
                chk.MergeAttribute("type", "checkbox");
                chk.MergeAttribute("name", action.HiddenInputId);

                Td.InnerHtml = chk.ToString();
            }
            else if (action.ActionType == ActionType.Action.CheckBoxAction)
            {
                // On determine l'identifiant de la ligne
                object pval = ModelHelper.GetPropertyValue(action.ActionIdentifier, objetAAfficher);
                string lineIdentifier = string.Empty;
                if (pval != null)
                    lineIdentifier = pval.ToString();

                var actionValue = action.EvaluateActionExpression((T)objetAAfficher);
                string actionUrl = actionValue.ToString();

                TagBuilder chk = new TagBuilder("input");
                chk.MergeAttribute("value", lineIdentifier);
                chk.MergeAttribute("type", "checkbox");
                chk.MergeAttribute("controltype", "ajaxAction");
                chk.MergeAttribute("actionUrl", actionUrl);

                // La check box est elle sélectionnée
                var IsChecked = action.EvaluateIsCheckedExpression((T)objetAAfficher);

                if (IsChecked.ToString().ToUpper() == "TRUE")
                {
                    chk.MergeAttribute("checked", "checked");
                }

                Td.InnerHtml = chk.ToString();
            }
            else if (action.ActionType == ActionType.Action.HyperLinkAction)
            {
                var actionValue = action.EvaluateActionExpression((T)objetAAfficher);
                string actionUrl = actionValue.ToString();

                TagBuilder chk = new TagBuilder("a");
                chk.MergeAttribute("href", "#");
                chk.MergeAttribute("controltype", "ajaxAction");
                chk.MergeAttribute("actionUrl", actionUrl);
                chk.InnerHtml = action.ActionIdentifier;

                Td.InnerHtml = chk.ToString();
            }
            else
            {
                var pVal = action.EvaluateActionExpression((T)objetAAfficher);
                val = pVal.ToString();

                Td.InnerHtml = val;
            }

            Td.MergeAttribute("actiontype", action.ActionType.ToString());
            InjectHTMLOptions(ref Td, htmloptions);
            tbodytr.InnerHtml = String.Format("{0}\n{1}", tbodytr.InnerHtml, Td.ToString());
        }

        #endregion
    }
}