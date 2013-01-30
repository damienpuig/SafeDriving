/*** BUTTON ***/
function ButtonClass()
{
	this.init = function (parentContainer)
	{
		$(parentContainer + " input[controltype=\"Button\"]").each(function ()
		{
			$(this).button();
		})
	}
}