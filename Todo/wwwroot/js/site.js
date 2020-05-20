// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function (e) {
	
	// locate each partial section.
	// if it has a URL set, load the contents into the area.
	
	$(".partialContents").each(function(index, item) {
        var emailAddress = $(item).data("email");
        if(emailAddress && emailAddress.length > 0)
        {
            $(item).load(`GravatarData?email=${emailAddress}`);
        }        
	});
});