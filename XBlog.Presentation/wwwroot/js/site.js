setTimeout(function () {
    var alerts = document.querySelectorAll('.alert');
    alerts.forEach(function (alert) {
        // اضافه کردن کلاس fade برای انیمیشن
        alert.classList.remove('show');
        alert.classList.add('fade');

        // حذف کامل از DOM بعد از انیمیشن
        setTimeout(function () {
            alert.remove();
        }, 500);
    });
}, 5000); // 5 ثانیه