$(document).ready(function(){
	userControl();
	$('.loginBtn').click(function(){
		var username = $.trim($('.loginUser').val());
		var password = $.trim($('.loginPassword').val());
		authenticateUser(username,password);
});
});