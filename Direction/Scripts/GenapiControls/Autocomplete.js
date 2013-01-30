/*** AUTOCOMPLETE CLASS ***/
function AutocompleteClass()
{
	// Initialisation des controls de ce type
	this.init = function (parentContainer)
	{
		$(parentContainer + " input[controltype=\"Autocomplete\"]").each(function ()
		{
			var renderItemOverride = function (ul, item)
			{
				return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append('<a>' + item.label + '</a>')
                        .appendTo(ul);
			};

			//Cache 
			var cache = {}, lastXhr;

			var sourcePath = $(this).attr("datasource");
			var hiddenElementId = $(this).attr("hiddenfieldid");
			var allowFreeInput = $(this).attr("allowfreeinput");
			var autoCompelteElement = $(this);

			autoCompelteElement.autocomplete({
				source: function (request, response)
				{
					if (request.term in cache)
					{
						response(cache[request.term]);
					}

					lastXhr = $.getJSON(sourcePath, request, function (data, status, xhr)
					{
						if (xhr === lastXhr)
						{
							cache[request.term] = data;
							response(data);
						}
					});
				},
				minLength: 0,
				delay: 200,
				select: function (event, ui)
				{
					var selectedObj = ui.item;
					$(autoCompelteElement).val(selectedObj.label);
					$('#' + hiddenElementId).val(selectedObj.id);
					autoCompelteElement.focus();

					$('#' + hiddenElementId).blur();
					$('#' + hiddenElementId).valid();
					return false;
				},
				change: function (event, ui)
				{
					if (!ui.item && allowFreeInput == "False")
					{
						autoCompelteElement.val("");
						$('#' + hiddenElementId).val("");
					}
					else if (allowFreeInput == "True")
					{
						$('#' + hiddenElementId).val(autoCompelteElement.val());
					}

					$('#' + hiddenElementId).blur();
					$('#' + hiddenElementId).valid();
				}
			}).data('autocomplete')._renderItem = renderItemOverride;

			if (allowFreeInput == "True")
			{
				autoCompelteElement.bind('keyup', function ()
				{
					$('#' + hiddenElementId).val(autoCompelteElement.val());
				});
			}

			var imgId = autoCompelteElement.attr("id") + "_img";
			autoCompelteElement.after('<img id="' + imgId + '" src="/Content/Images/dropdown_arrow.JPG" style="position:relative;top:5px;"/>');
			$("#" + imgId).click(function ()
			{
				autoCompelteElement.val("");
				autoCompelteElement.focus();
				autoCompelteElement.autocomplete('search');
			});
		})
	}
}