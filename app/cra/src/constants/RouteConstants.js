const routes = Object.freeze({
  backend: {
    weatherForecast: `${process.env.REACT_APP_BFF_URL}/api/WeatherForecast`,
    login: `${process.env.REACT_APP_BFF_URL}/bff`,
    logout: `${process.env.REACT_APP_BFF_URL}/bff/user/logout`,
    userInfo: `${process.env.REACT_APP_BFF_URL}/bff/user/info`,
    post: `${process.env.REACT_APP_BFF_URL}/api/Post`,
    labelText: `${process.env.REACT_APP_BFF_URL}/api/LabelText`,
    postalCode: `${process.env.REACT_APP_BFF_URL}/api/PostalCode`,
    postCategory: `${process.env.REACT_APP_BFF_URL}/api/PostCategory`,
    user: `${process.env.REACT_APP_BFF_URL}/api/User`,
    register: `${process.env.REACT_APP_IDP_URL}/Account/Register`,
  },
  frontend: {
    slash: "/",
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
