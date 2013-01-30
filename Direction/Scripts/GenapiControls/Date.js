/*** DATE ***/
function DateClass()
{
	this.init = function (parentContainer)
	{
		$(parentContainer + " input[controltype=\"Date\"]").each(function ()
		{
			var self = $(this);
			self.datepicker(
			{
				changeMonth: true,
				changeYear: true,
				yearRange: "1900:+0",
				minDate: "-150Y",
				maxDate: "+0D",
				showOn: "button",
				buttonImage: "/Content/Images/calendar.gif",
				buttonImageOnly: true,
				dateFormat: 'dd/mm/yy',
				onSelect: function (dateText, inst)
				{
					self.blur();
					self.valid();
				}
			});
		});
	}
}
