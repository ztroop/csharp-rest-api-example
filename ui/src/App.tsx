import React, { useState, useEffect, Fragment } from 'react'
import {
  deleteCustomer,
  getCustomers,
  setCustomer,
  updateCustomer,
} from './actions/Customers'
import AddUserForm from './components/AddUserForm'
import EditUserForm from './components/EditUserForm'
import UserTable from './components/UserTable'
import { User, ID } from './types'

const App = () => {
  const [users, setUsers] = useState<User[]>([])
  const [currentUser, setCurrentUser] = useState({
    id: '',
    firstname: '',
    lastname: '',
  })
  const [editing, setEditing] = useState(false)

  useEffect(() => {
    getCustomers()
      .catch((err) => console.log(err))
      .then((resp) => {
        setUsers(resp)
      })
  }, [])

  const addUser = (user: User) => {
    setCustomer(user)
      .catch((err) => console.log(err))
      .then((resp) => {
        const userResult: User = resp
        setUsers([...users, userResult])
      })
  }

  const deleteUser = (id: ID) => {
    setEditing(false)

    deleteCustomer(id)
      .catch((err) => console.log(err))
      .then((resp) => {
        if (resp === 204) {
          setUsers(users.filter((user) => user.id !== id))
        }
      })
  }

  const updateUser = (id: ID, updatedUser: User) => {
    setEditing(false)

    updateCustomer(id, updatedUser)
      .catch((err) => console.log(err))
      .then((resp) => {
        setUsers(users.map((user) => (user.id === id ? resp : user)))
      })
  }

  const editRow = (user: User) => {
    setEditing(true)

    setCurrentUser({ ...user })
  }

  return (
    <div className="container">
      <h1>Customer Management</h1>
      <div className="flex-row">
        <div className="flex-large">
          {editing ? (
            <Fragment>
              <h2>Update Customer</h2>
              <EditUserForm
                editing={editing}
                setEditing={setEditing}
                currentUser={currentUser}
                updateUser={updateUser}
              />
            </Fragment>
          ) : (
            <Fragment>
              <h2>Add Customer</h2>
              <AddUserForm addUser={addUser} />
            </Fragment>
          )}
        </div>
        <div className="flex-large">
          <h2>Customer List</h2>
          <UserTable users={users} editRow={editRow} deleteUser={deleteUser} />
        </div>
      </div>
    </div>
  )
}

export default App
