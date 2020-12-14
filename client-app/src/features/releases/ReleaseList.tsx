import React, { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'

type Release = {
    CreatedOn: Date
    Name: string
}

export const ReleaseList = () => {    
    let { project } = useParams<{ project: string }>()
    const [projectList, setProjectList] = useState<Release[]>()
    const [isLoading, setIsLoading] = useState(true)

    const getData = async () => {
        let response = await fetch(`http://localhost:8080/releases/${project}`)
        setProjectList(await response.json())
        setIsLoading(false)
    }

    useEffect(() => {
        setIsLoading(true)
        getData()
        // eslint-disable-next-line
    }, [])

    return (    
        <div>
            <h1><Link to="/">Projects</Link> / Releases</h1>
            <div className="py-8">
                {!isLoading && projectList &&
                    <table className="min-w-full">
                        <tbody className="bg-white divide-y divide-gray-200">
                            {projectList && projectList.map(f => (
                                <tr key={f.Name}>
                                    <td>{f.Name}</td>
                                    <td>{new Date(f.CreatedOn).toDateString()}</td>
                                </tr>
                            ))}
                        </tbody>
                </table>}
                {!isLoading && (!projectList || (projectList && projectList.length === 0)) &&
                    <span>No data</span>}
                {isLoading && 
                <span>Loading...</span>}
            </div>
        </div>
    )
}