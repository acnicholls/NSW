export const defaultLabelText = {
  id: "Empty",
  english: "Emtpy",
  japanese: "Empty",
};

export const defaultPostCategory = {
  id: -1,
  englishTitle: "Empty",
  japaneseTitle: "Empty",
  englishDescription: "Empty",
  japaneseDescription: "Empty",
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
  postUser: null,
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
  cookiename: "Anonymous User",
  id: -1,
  name: "Anonymous User",
  firstName: "Anonymous",
  lastName: "User",
  username: "Anon",
  role: "Empty",
  languagePreference: -1,
  isAuthenticated: false,
};
