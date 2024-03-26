const routes = Object.freeze({
  public: `${process.env.REACT_APP_PUBLIC_URL}`,
  backend: {
    login: `${process.env.REACT_APP_BFF_URL}/bff`,
    logout: `${process.env.REACT_APP_BFF_URL}/bff/user/logout`,
    userInfo: `${process.env.REACT_APP_BFF_URL}/bff/user/info`,
    user: `${process.env.REACT_APP_BFF_URL}/api/User`,
    privatePost: `${process.env.REACT_APP_BFF_URL}/api/Post`,
    publicPost: `${process.env.REACT_APP_BFF_URL}/bff/Post`,
    privateLabelTextGroup: `${process.env.REACT_APP_BFF_URL}/api/LabelText/group`,
    publicLabelTextGroup: `${process.env.REACT_APP_BFF_URL}/bff/LabelText/group`,
    privatePostCategory: `${process.env.REACT_APP_BFF_URL}/api/PostCategory`,
    publicPostCategory: `${process.env.REACT_APP_BFF_URL}/bff/PostCategory`,
    postalCode: `${process.env.REACT_APP_BFF_URL}/api/PostalCode`,
    labelText: `${process.env.REACT_APP_BFF_URL}/api/LabelText`,
    register: `${process.env.REACT_APP_IDP_URL}/Account/Register`,
  },
  frontend: {
    slash: "/",
    splash: "/splash",
    index: "/index",
    about: "/about",
    denied: "/denied",
    search: "/search",
    posts: "/posts",
    postById: "/posts/:postId",
    postCategoryByName: "/posts/category/:categoryName",
    postCategoryById: "/posts/category/:categoryId",
    myPosts: "/my-posts",
    userDetails: "/user-details",
    register: "/register",
    login: "/login",
    logout: "/logout",
    loggedIn: "/loggedin",
    loggedOut: "/loggedout",
    admin: {
      labelText: "/admin/label-text",
      postCategory: "admin/post-category",
      users: "/admin/users",
    },
  },
});

export default routes;
