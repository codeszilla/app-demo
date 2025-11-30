window.cookieHelper = {
    setCookie: function (name, value, hours) {
        const d = new Date();
        d.setTime(d.getTime() + (hours * 60 * 60 * 1000));
        let expires = "expires=" + d.toUTCString();
        document.cookie = `${name}=${value}; ${expires}; path=/; Secure; SameSite=None`;
    },

    getCookie: function (name) {
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for (let c of ca) {
            while (c.charAt(0) === ' ') c = c.substring(1);
            if (c.indexOf(nameEQ) === 0)
                return c.substring(nameEQ.length);
        }
        return "";
    },

    deleteCookie: function (name) {
        document.cookie = `${name}=; Max-Age=0; path=/;`;
        document.cookie = `${name}=; Max-Age=0; path=/; Secure; SameSite=None`;
    }
};
