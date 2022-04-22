mergeInto(LibraryManager.library, {

  GetJWTToken: function () {
    return localStorage.getItem("token")
  },

});