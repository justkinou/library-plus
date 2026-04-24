"use client";

import React from "react";
import EditButton from "./EditButton";

function UserInfo() {
  let imgLink =
    "https://i.pinimg.com/736x/4b/61/cf/4b61cf98bfc8e92c935e9f4fe3011f08.jpg";
  let email = "example@mail.com";
  let phoneNumber = "+375 25 742 06 84";
  let joinDate = "14 June 2026";
  return (
    <div>
      <div className="mb-3 flex items-center justify-between">
        <h2 className="text-xl font-semibold">Account Info</h2>
        <EditButton />
      </div>
      <div className="flex items-center gap-4">
        <img src={imgLink} className="size-24 rounded-full"></img>
        <div className="flex flex-col">
          <h3>Email: {email}</h3>
          <h3>Phone number: {phoneNumber}</h3>
          <h3>Joined: {joinDate}</h3>
        </div>
      </div>
    </div>
  );
}

export default UserInfo;
