$.extend( $.fn.dataTable.defaults, {
    responsive: true,
    searching: false,
    ordering: false,
    processing: true,
    serverSide: true,
    //order: [[1, "asc"]],
    language: {
        url: '/vendors/datatables.net/i18n/vi_VN.js'
    },
    dom: `  <"row"
                    <"col-md-4"l>
                    <"col-md-4"i>
                    <"col-md-4"p>>
            <"row"frt>
            <"row"
                    <"col-md-4"l>
                    <"col-md-4"i>
                    <"col-md-4"p>>
    `,
    drawCallback: function (settings) {
        this.api().responsive.recalc();
    }
} );