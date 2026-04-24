import React from "react";
import EditButton from "./EditButton";

function DeliveryAddress() {
  let city = "Łódź";
  let street = "Aleje Politechniki";
  let buildingNumber = "9A";
  let postCode = "93-590";
  return (
    <div className="px-8">
      <div>
        <div className="mb-3 flex items-center justify-between">
          <h2 className="text-xl font-semibold">Preferred delivery address</h2>
          <EditButton />
        </div>
      </div>
      <div className="flex items-center gap-4">
        <div className="flex flex-col text-base">
          <div className="inline-flex gap-6 mb-2">
            <h4>City: {city}</h4>
            <h4>Street: {street}</h4>
            <h4>Building number: {buildingNumber}</h4>
          </div>
          <h4>Post code: {postCode}</h4>
        </div>
      </div>
    </div>
  );
}

export default DeliveryAddress;
