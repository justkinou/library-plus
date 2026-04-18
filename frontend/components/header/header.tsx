import React from 'react'
import HeaderActions from './actions/actions'
import HeaderLinks from './links'
import Logo from './logo'
import HeaderUser from './user/user'

function Header() {
  return (
    <div className="w-full flex items-center justify-between py-2">
      <Logo />

      <div className="flex items-center justify-center gap-12">
        <HeaderLinks />

        <HeaderActions />

        <HeaderUser />
      </div>
    </div>
  )
}

export default Header