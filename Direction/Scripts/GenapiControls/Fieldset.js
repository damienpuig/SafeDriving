/*** FIELDSET ***/
function FieldsetClass()
{
	this.init = function (parentContainer)
	{
		var self = this;
		$(parentContainer + " .fieldsetgroup").each(function ()
		{
			self.initFieldSetGroupElement($(this));
		});
	}

	// FieldSet: Récupère le plus haut fieldset et donne sa hauteur aux autres fieldset du groupe
	this.initFieldSetGroupElement = function(fieldsetGrp)
	{
		var height = this.getMaxFieldsetHeightOnFieldsetGroup(fieldsetGrp);
		$(fieldsetGrp).find("fieldset").each(function ()
		{
			$(this).height(height);
		});
	}

	// Retourne la hauteur du plus grand fieldset du groupe
	this.getMaxFieldsetHeightOnFieldsetGroup = function(fieldsetGrp)
	{
		var maxHeight = 50;
		$(fieldsetGrp).find("fieldset").each(function ()
		{
			if ($(this).height() > maxHeight)
				maxHeight = $(this).height();
		});

		return maxHeight;
	}
}