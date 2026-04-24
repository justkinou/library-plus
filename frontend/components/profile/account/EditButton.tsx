"use client";

import { PencilSimpleIcon } from "@phosphor-icons/react";
import React from "react";

function EditButton() {
  return (
    <button
      type="button"
      className="inline-flex items-center gap-2 border border-dark px-1.5 py-0.5 cursor-pointer"
    >
      <PencilSimpleIcon size={18} />
      <span>Edit</span>
    </button>
  );
}

export default EditButton;
