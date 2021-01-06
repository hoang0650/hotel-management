(function () {

	var alertService = {
		showAlert: showAlert,
		success: success,
		info: info,
		warning: warning,
		error: error
	};

	window.alerts = alertService;

	var alertContainer = $(".alert-container");

	function showAlert(message, alertClass) {
	    javascript: Notify(message, 'top-right', '5000', alertClass, 'fa-list', true); return false;
	}

	function success(message) {
		showAlert({ alertClass: "alert-success", message: message });
	}

	function info(message) {
		showAlert({ alertClass: "alert-info", message: message });
	}

	function warning(message) {
		showAlert({ alertClass: "alert-warning", message: message });
	}

	function error(message) {
		showAlert({ alertClass: "alert-danger", message: message });
	}

})();
