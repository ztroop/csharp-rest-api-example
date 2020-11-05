import React from 'react'
import { User } from '../types'

const UserTable: React.FC<any> = (props) => (
  <table>
    <thead>
      <tr>
        <th>First Name</th>
        <th>Last Name</th>
        <th>Actions</th>
      </tr>
    </thead>
    <tbody>
      {(props.users?.length > 0) ? (
        props.users.map((user: User) => (
          <tr key={user.id}>
            <td>{user.firstname}</td>
            <td>{user.lastname}</td>
            <td>
              <button
                onClick={() => {
                  props.editRow(user)
                }}
                className="button muted-button"
              >
                Update
              </button>
              <button
                onClick={() => props.deleteUser(user.id)}
                className="button muted-button"
              >
                Delete
              </button>
            </td>
          </tr>
        ))
      ) : (
        <tr>
          <td colSpan={3}>No Customers</td>
        </tr>
      )}
    </tbody>
  </table>
)

export default UserTable
