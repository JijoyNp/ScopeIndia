//$(document).ready(function () {
//    // Countries
//    var country_arr = new Array("Select Country", "AUSTRALIA", "INDIA", "NEW ZEALAND", "USA", "UAE", "MAURITIUS");

//    $.each(country_arr, function (i, item) {
//        $('#Country').append($('<option>', {
//            value: item,
//            text: item,
//        }, '</option>'));
//    });

//    // States
//    var s_a = new Array();
//    s_a[0] = "Select State";
//    s_a[1] = "Select State|QUEENSLAND|VICTORIA";
//    s_a[2] = "Select State|ANDHRAPRADESH|KARNATAKA|TAMILNADU|DELHI|GOA|W-BENGAL|GUJARAT|MADHYAPRADESH|MAHARASHTRA|RAJASTHAN|KERALA";
//    s_a[3] = "Select State|AUCKLAND";
//    s_a[4] = "Select State|NEWJERSEY|ILLINOIS";
//    s_a[5] = "Select State|DUBAI";
//    s_a[6] = "Select State|MAURITIUS";

//    // Cities
//    var c_a = new Array();
//    c_a['QUEENSLAND'] = "BRISBANE";
//    c_a['VICTORIA'] = "MELBOURNE";
//    c_a['ANDHRAPRADESH'] = "HYDERABAD";
//    c_a['KARNATAKA'] = "BANGLORE";
//    c_a['TAMILNADU'] = "CHENNAI";
//    c_a['DELHI'] = "DELHI";
//    c_a['GOA'] = "GOA";
//    c_a['W-BENGAL'] = "KOLKATA";
//    c_a['GUJARAT'] = "AHMEDABAD1|AHMEDABAD2|AHMEDABAD3|BARODA|BHAVNAGAR|MEHSANA|RAJKOT|SURAT|UNA";
//    c_a['MADHYAPRADESH'] = "INDORE";
//    c_a['MAHARASHTRA'] = "MUMBAI|PUNE";
//    c_a['RAJASTHAN'] = "ABU";
//    c_a['AUCKLAND'] = "AUCKLAND";
//    c_a['NEWJERSEY'] = "EDISON";
//    c_a['ILLINOIS'] = "CHICAGO";
//    c_a['MAURITIUS'] = "MAURITIUS";
//    c_a['DUBAI'] = "DUBAI";
//    c_a['KERALA'] = "TRIVANDRUM|MALAPPURAM|WAYANAD|KANNUR|KOZHIKKODE|PATHANAMTHITTA|KOTTAYAM|IDUKKI|KASARGOD|ALAPPUZHA|PALAKKAD|THRISSUR|ERNAMKULAM|KOLLAM";


//    $('#Country').change(function () {
//        var c = $(this).val();
//        var state_arr = s_a[c].split("|");
//        $('#State').empty();
//        $('#City').empty();
//        if (c == 0) {
//            $('#State').append($('<option>', {
//                value: '0',
//                text: 'Select State',
//            }, '</option>'));
//        } else {
//            $.each(state_arr, function (i, item_state) {
//                $('#State').append($('<option>', {
//                    value: item_state,
//                    text: item_state,
//                }, '</option>'));
//            });
//        }
//        $('#City').append($('<option>', {
//            value: '0',
//            text: 'Select City',
//        }, '</option>'));
//    });

//    $('#State').change(function () {
//        var s = $(this).val();
//        if (s == 'Select State') {
//            $('#City').empty();
//            $('#City').append($('<option>', {
//                value: '0',
//                text: 'Select City',
//            }, '</option>'));
//        }
//        var city_arr = c_a[s].split("|");
//        $('#City').empty();

//        $.each(city_arr, function (j, item_city) {
//            $('#City').append($('<option>', {
//                value: item_city,
//                text: item_city,
//            }, '</option>'));
//        });


//    });
//});



$(document).ready(function () {
    // Countries
    var country_arr = ["Select Country", "AUSTRALIA", "INDIA", "NEW ZEALAND", "USA", "UAE", "MAURITIUS"];

    $.each(country_arr, function (i, item) {
        $('#Country').append($('<option>', {
            value: item, // Use country name as the value
            text: item,
        }));
    });

    // States
    var s_a = {
        "Select Country": "Select State",
        "AUSTRALIA": "Select State|QUEENSLAND|VICTORIA",
        "INDIA": "Select State|ANDHRAPRADESH|KARNATAKA|TAMILNADU|DELHI|GOA|W-BENGAL|GUJARAT|MADHYAPRADESH|MAHARASHTRA|RAJASTHAN|KERALA",
        "NEW ZEALAND": "Select State|AUCKLAND",
        "USA": "Select State|NEWJERSEY|ILLINOIS",
        "UAE": "Select State|DUBAI",
        "MAURITIUS": "Select State|MAURITIUS"
    };

    // Cities
    var c_a = {
        "QUEENSLAND": "BRISBANE",
        "VICTORIA": "MELBOURNE",
        "ANDHRAPRADESH": "HYDERABAD",
        "KARNATAKA": "BANGLORE",
        "TAMILNADU": "CHENNAI",
        "DELHI": "DELHI",
        "GOA": "GOA",
        "W-BENGAL": "KOLKATA",
        "GUJARAT": "AHMEDABAD1|AHMEDABAD2|AHMEDABAD3|BARODA|BHAVNAGAR|MEHSANA|RAJKOT|SURAT|UNA",
        "MADHYAPRADESH": "INDORE",
        "MAHARASHTRA": "MUMBAI|PUNE",
        "RAJASTHAN": "ABU",
        "AUCKLAND": "AUCKLAND",
        "NEWJERSEY": "EDISON",
        "ILLINOIS": "CHICAGO",
        "DUBAI": "DUBAI",
        "MAURITIUS": "MAURITIUS",
        "KERALA": "TRIVANDRUM|MALAPPURAM|WAYANAD|KANNUR|KOZHIKKODE|PATHANAMTHITTA|KOTTAYAM|IDUKKI|KASARGOD|ALAPPUZHA|PALAKKAD|THRISSUR|ERNAMKULAM|KOLLAM"
    };

    $('#Country').change(function () {
        var c = $(this).val(); // Get the selected country name
        var state_arr = s_a[c] ? s_a[c].split("|") : ["Select State"];
        $('#State').empty();
        $('#City').empty();

        // Populate the states dropdown
        $.each(state_arr, function (i, item_state) {
            $('#State').append($('<option>', {
                value: item_state,
                text: item_state,
            }));
        });

        // Reset the cities dropdown
        $('#City').append($('<option>', {
            value: '0',
            text: 'Select City',
        }));
    });

    $('#State').change(function () {
        var s = $(this).val(); // Get the selected state name
        var city_arr = c_a[s] ? c_a[s].split("|") : ["Select City"];
        $('#City').empty();

        // Populate the cities dropdown
        $.each(city_arr, function (i, item_city) {
            $('#City').append($('<option>', {
                value: item_city,
                text: item_city,
            }));
        });
    });
    
});



    function closeViewBagMessage() {
        var alertBox = document.getElementById('viewbagAlert');
    if (alertBox) {
        alertBox.style.display = 'none';
        }
    }

    // Auto-close after 7 seconds
    setTimeout(function () {
        closeViewBagMessage();
    }, 7000);

