$(function () {

    const overlay = $('#overlay')
    const content = $('#modalContent')
    function setLoading() {
        content.html('載入中...')
    }

    function setError() {
        content.html('載入失敗，請稍後再試')
    }

    ///* ===== 點擊訂單 ===== */
    //  $(document).on('click', '.order-item', function () {
    //      var id = $(this).data('id');
    //      console.log('點擊 order id:', id);
    //      $('#orderModal').modal('show'); // 假設你用 Bootstrap modal
    //      // 這裡可以放 AJAX 取得詳細訂單，填入 modal
    //  });


    //  $.ajax({
    //    url: `/api/orders/` + id,   // 之後直接接後端 API
    //    method: 'GET',
    //    dataType: 'json',
    //      success: function (order) { 
    //      console.log(order);
    //    },
    //      error: function(order) {
    //          alert('找不到這筆訂單');

    //    }
    //  })

    //})



    /* ===== 開關 Modal ===== */
    function openModal() {
        overlay.addClass('show')
    }

    function closeModal() {
        overlay.removeClass('show')
        content.html('')
    }

    /* ===== 關閉事件 ===== */
    $('#closeBtn').on('click', closeModal)

    overlay.on('click', function (e) {
        if (e.target === this) {
            closeModal()
        }
    })
}
)
