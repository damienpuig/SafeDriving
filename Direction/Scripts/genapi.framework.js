// Appelé au chargement de la page, initialise l'ensemble des controles
/**** GENAPI JAVASCRIPT FRAMEWORK ****/
function GenapiFrameworkClass()
{
	this.globalHandler;
	this.autocompleteHandler;
	this.clockHandler;
	this.dateHandler;
	this.buttonHandler;
	this.fieldsetHandler;
	this.gridHandler;
	this.popupHandler;
	this.timeHandler;
	this.checkboxlistHandler;
	this.uppercase;

	this.init = function ()
	{
		var parentContainer = "body";

		// Global init
		this.globalHandler = new GlobalClass();

		// Initialisation des controls de date
		this.dateHandler = new DateClass();

		// Initialisation des controls Autocomplete
		this.autocompHandler = new AutocompleteClass();

		// Initialisation des control de type Button
		this.buttonHandler = new ButtonClass();

		// Initialisation des FieldSetGroups
		this.fieldsetHandler = new FieldsetClass();

		// Initialisation des Grilles
		this.gridHandler = new GridClass();

		// Init des actions de popup
		this.popupHandler = new PopupClass();

		// CheckBox List
		this.checkboxlistHandler = new CheckBoxListClass();

		// uppercase textboxes
		this.uppercase = new Uppercase();

		// Appel de l'init des controls
		this.reinitControls(parentContainer);
	}

	this.reinitControls = function (parentContainer)
	{
		this.globalHandler.initFormEvent(parentContainer);
		this.globalHandler.initRequiredField(parentContainer);
		this.globalHandler.initControls(parentContainer);
		this.dateHandler.init(parentContainer);
		this.autocompHandler.init(parentContainer);
		this.buttonHandler.init(parentContainer);
		this.fieldsetHandler.init(parentContainer);
		this.gridHandler.init(parentContainer, this);
		this.popupHandler.init(this);
		this.checkboxlistHandler.init(parentContainer);
		this.uppercase.init(parentContainer);
	}

	this.reinitPopupActions = function (parentContainer)
	{
		this.popupHandler.init(this);
	}
}