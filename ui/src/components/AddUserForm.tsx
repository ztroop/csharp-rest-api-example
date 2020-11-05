import React, { useState } from 'react'

const AddUserForm: React.FC<any> = (props) => {
  const initialFormState = { id: null, firstname: '', lastname: '' }
  const [user, setUser] = useState(initialFormState)

  const handleInputChange = (event: any) => {
    const { name, value } = event.target

    setUser({ ...user, [name]: value })
  }

  return (
    <form
      onSubmit={(event) => {
        event.preventDefault()
        if (!user.firstname || !user.lastname) return

        props.addUser(user)
        setUser(initialFormState)
      }}
    >
      <label>First Name</label>
      <input
        type="text"
        name="firstname"
        value={user.firstname}
        onChange={handleInputChange}
      />
      <label>Last Name</label>
      <input
        type="text"
        name="lastname"
        value={user.lastname}
        onChange={handleInputChange}
      />
      <button>Create User</button>
    </form>
  )
}

export default AddUserForm
