export const defaultLabelText = {
  id: "Empty",
  text: "Emtpy",
};

export const defaultPostCategory = {
  id: -1,
  title: "Empty",
  description: "Empty",
};

export const defatulPostalCode = {
  code: "Empty",
  longitude: -1,
  latitude: -1,
};

export const defaultPost = {
  id: -1,
  categoryId: -1,
  title: "Empty",
  description: "Empty",
  price: -1,
  expiry: "2023-10-02",
  userId: -1,
  status: "Empty",
  deleteFlag: false,
  postUser: {},
};

/*
  this is the C# representation of the user (currently)
*/

// var user = new
// {
// cookiename = User.Identity.Name,
// Id = response.Claims.FirstOrDefault(x => x.Type == "sub").Value,
// Name = response.Claims.FirstOrDefault(x => x.Type == "name").Value,
// FirstName = response.Claims.FirstOrDefault(x => x.Type == "given_name").Value,
// LastName = response.Claims.FirstOrDefault(x => x.Type == "family_name").Value,
// Username = response.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value,
// Role = "MEMBER", // get these from the user API?
// LanguagePreference = -1, //
// IsAuthenticated = true,
// };

export const anonymousUser = {
  id: -1,
  userName: "Anonymous User",
  email: "Anon@central.serv",
  phone: "99999999",
  role: "Empty",
  postalCode: {},
  languagePreference: 1,
  isAuthenticated: false,
};
