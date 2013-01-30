/*** GRID ***/
function GridClass()
{
	this.init = function (parentContainer, frkHandler)
	{
		$(parentContainer + " table[controltype=\"Grid\"]").each(function ()
		{
			$(this).dataTable(
			{
				"bStateSave ": true,
				"sPaginationType": "full_numbers",
				"iDisplayLength": 50,
				"bPaginate": true,
				"bLengthChange": true,
				"bAutoWidth": true,
				"bJQueryUI": true,
				"bSort": true,
				"fnDrawCallback": function ()
				{
					// A chaque changement de page, on réinitialise les control de type open popup
					frkHandler.reinitPopupActions(parentContainer);
				},
	            "oLanguage": {
	                "sProcessing": "Traitement en cours...",
	                "sLengthMenu": "Afficher _MENU_ éléments",
	                "sZeroRecords": "Aucun élément à afficher",
	                "sInfo": "Affichage de l'élement _START_ à _END_ sur _TOTAL_ éléments",
	                "sInfoEmpty": "Affichage de l'élement 0 à 0 sur 0 éléments",
	                "sInfoFiltered": "(filtré de _MAX_ éléments au total)",
	                "sInfoPostFix": "",
	                "sSearch": "Rechercher :",
	                "sUrl": "",
	                "oPaginate": {
	                    "sFirst": "Premier",
	                    "sPrevious": "Précédent",
	                    "sNext": "Suivant",
	                    "sLast": "Dernier"
	                }
	            }
			});
		});
	}
}