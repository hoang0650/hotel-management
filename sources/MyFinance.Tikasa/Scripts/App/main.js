require.config({

    baseUrl: "",

    // alias libraries paths
    paths: {
        'angular': '/Scripts/angular',
        'global': '/Scripts/global',
        'jquery': '/Scripts/jquery-1.10.2',
        'bootstrap': 'Scripts/bootstrap',
        'jquery-ui': 'Scripts/jquery-ui',
        'chosen.jquery': 'Scripts/chosen.jquery',
        'toastr':'Scripts/toastr',
        'appModule': '/Scripts/App/appModule',
        'ui-bootstrap': 'Scripts/ui-bootstrap-tpls-0.11.0',
       
    },

    //// Add angular modules that does not support AMD out of the box, put it in a shim
    shim: {
        'angular': ['jquery'],
        'toastr': ['jquery'],
        'jquery.gritter': ['jquery'],
        'bootstrap': ['jquery'],
        'ace-elements': ['jquery'],
        'chosen.jquery': ['jquery'],
        'ace-js': ['jquery'],
        'appModule': ['angular'],
        'angularAMD': ['angular'],
        'angular-route': ['angular'],
        'blockUI': ['angular'],
        'angular-sanitize': ['angular'],
        'ui-bootstrap': ['angular'],
        'angucomplete-alt': ['angular'],
        'daypilot': ['angular'],
        'cfp.hotkeys': ['angular']

    },

    // kick start application
    deps: ['appModule']
});

require(['jquery' ], function () {
    //Init Data
   
});
