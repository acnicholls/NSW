export const userQueryKeys = {
  user: ["user"],
  getUserInfo: () => [...userQueryKeys.user, "info"],
};
